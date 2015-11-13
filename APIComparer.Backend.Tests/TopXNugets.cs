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

        public TopXNugets()
        {
            repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            packageManager = new PackageManager(repo, "packages");
        }

        [Test]
        public void Should_calculate_stats_for_top_10_packages()
        {
            var frameworks = new []
            {
                ".NETFramework,Version=v4.5",
                ".NETFramework,Version=v4.0"
            };

            var packages = repo.Search(String.Empty, frameworks, false)
                .Skip(0)
                .Take(25)
                .ToList();

            var output = new List<PackageResults>();

            foreach (var pkg in packages)
            {
                Console.WriteLine($"Processing package {pkg.Id}...");

                var dotNetRefs = pkg.AssemblyReferences.OfType<PhysicalPackageAssemblyReference>()
                    .Where(pkgRef => frameworks.Contains(pkgRef.TargetFramework?.FullName))
                    .ToList();

                if (!dotNetRefs.Any())
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

                packageManager.InstallPackage(pkg.Id, major.Version, true, false);
                packageManager.InstallPackage(pkg.Id, minor.Version, true, false);
                packageManager.InstallPackage(pkg.Id, latest.Version, true, false);

                var minorDiff = Diff(pkg, minor.Version, latest.Version);
                var majorDiff = Diff(pkg, major.Version, latest.Version);

                output.Add(new PackageResults()
                {
                    Id = pkg.Id,
                    CurrentVersion = latest.Version,
                    MajorVersion = major.Version,
                    MinorVersion = minor.Version,
                    MinorDiff = minorDiff,
                    MajorDiff = majorDiff
                });

                if (output.Count == 10)
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("== Output ==");
            foreach (var res in output)
                Console.WriteLine($"{res.Id} {res.MajorVersion} {res.MinorVersion} {res.CurrentVersion}");
        }

        internal class PackageResults
        {
            public string Id { get; set; }
            public SemanticVersion CurrentVersion { get; set; }
            public SemanticVersion MinorVersion { get; set; }
            public SemanticVersion MajorVersion { get; set; }
            public object MinorDiff { get; set; }
            public object MajorDiff { get; set; }
        }

        private object Diff(IPackage pkg, SemanticVersion left, SemanticVersion right)
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
            return ViewModelBuilder.Build(packageDescription, diffedCompareSets);
        }
    }
}
