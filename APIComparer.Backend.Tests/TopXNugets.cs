namespace APIComparer.Backend.Tests
{
    using NuGet;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Reporting;
    using VersionComparisons;

    [TestFixture]
    public class TopXNugets
    {
        IPackageRepository repo;
        IPackageManager packageManager;
        readonly string[] frameworks;

        public TopXNugets()
        {
            repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            packageManager = new PackageManager(repo, "packages");

            frameworks = new[]
            {
                ".NETFramework,Version=v4.5",
                ".NETFramework,Version=v4.0"
            };
        }

        [Test]
        public void Should_calculate_stats_for_top_25_packages()
        {
            var packages = repo.Search(String.Empty, frameworks, false)
                .Skip(0)
                .Take(50)
                .ToList();

            var output = new List<PackageResults>();

            foreach (var pkg in packages)
            {
                Console.WriteLine($"Processing package {pkg.Id}...");

                if (!HasDotNetAssemblies(pkg))
                {
                    Console.WriteLine("    Not a .NET package");
                    continue;
                }

                var versions = repo.FindPackagesById(pkg.Id)
                    .FilterByPrerelease(false)
                    .OrderByDescending(p => p.Version)
                    .ToList();

                var latest = versions.First();
                var minor = versions.Last(p => p.Version.Version.Major == latest.Version.Version.Major && p.Version.Version.Minor == latest.Version.Version.Minor);
                var major = versions.Last(p => p.Version.Version.Major == latest.Version.Version.Major);

                if (!HasDotNetAssemblies(minor) && !HasDotNetAssemblies(major))
                {
                    Console.WriteLine("    Lastest major/minor not .NET packages");
                    continue;
                }

                packageManager.InstallPackage(pkg.Id, major.Version, true, false);
                packageManager.InstallPackage(pkg.Id, minor.Version, true, false);
                packageManager.InstallPackage(pkg.Id, latest.Version, true, false);

                var pkgResult = new PackageResults()
                {
                    Id = pkg.Id,
                    CurrentVersion = latest.Version,
                    Major = new VersionReport(pkg.Id, latest.Version, major.Version),
                    Minor = new VersionReport(pkg.Id, latest.Version, minor.Version)
                };

                if (HasDotNetAssemblies(minor))
                    pkgResult.Minor.Diff = Diff(pkg, minor.Version, latest.Version);
                if (HasDotNetAssemblies(major))
                    pkgResult.Major.Diff = Diff(pkg, major.Version, latest.Version);

                output.Add(pkgResult);

                if (output.Count == 25)
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("| Package Name | Current Version | Latest Minor | Latest Major |");
            Console.WriteLine("| --------------|:---------------:|:------------------:|:-------------------:|");
            foreach (var res in output)
            {
                Console.WriteLine($"| {res.Id} | {res.CurrentVersion} | {res.Minor.Report()} | {res.Major.Report()} |");
            }
        }

        internal class PackageResults
        {
            public string Id { get; set; }
            public SemanticVersion CurrentVersion { get; set; }
            public VersionReport Minor { get; set; }
            public VersionReport Major { get; set; }
        }

        internal class VersionReport
        {
            public string PackageId { get; }
            public SemanticVersion Version { get; }
            public SemanticVersion Current { get; }
            public TargetReport Diff { get; set; }

            public VersionReport(string packageId, SemanticVersion current, SemanticVersion version)
            {
                PackageId = packageId;
                Current = current;
                Version = version;
            }

            public string Report()
            {
                if (Diff == null)
                    return $"<span title=\"Comparing {Version}...{Current}\">Not .NET</span>";

                return $"<a title=\"Comparing {Version}...{Current}\" href=\"http://apicomparer.particular.net/Compare/{PackageId}/{Version}...{Current}\">{Diff.BreakingChanges}</a>";
            }
        }

        TargetReport Diff(IPackage pkg, SemanticVersion left, SemanticVersion right)
        {
            var creator = new CompareSetCreator();
            var differ = new CompareSetDiffer();

            var packageDescription = new PackageDescription
            {
                PackageId = pkg.Id,
                Versions = new VersionPair(left.ToString(), right.ToString())
            };

            var compareSets = creator.Create(packageDescription);
            var diffedCompareSets = differ.Diff(compareSets);
            var vm = ViewModelBuilder.Build(packageDescription, diffedCompareSets);

            foreach (var framework in frameworks)
            {
                var target = vm.targets.FirstOrDefault(trg => trg.Name == framework);
                if (target != null)
                    return target;
            }

            return null;
        }

        bool HasDotNetAssemblies(IPackage pkg)
        {
            return pkg.AssemblyReferences
                .OfType<PhysicalPackageAssemblyReference>()
                .Any(pkgRef => frameworks.Contains(pkgRef.TargetFramework?.FullName));
        }
    }
}
