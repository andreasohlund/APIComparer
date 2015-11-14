namespace APIComparer.Backend
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using APIComparer.Shared;
    using NuGet;

    internal class NuGetDownloader
    {
        public NuGetDownloader(string nugetName, IEnumerable<string> repositories)
        {
            package = nugetName;

            var nugetCacheDirectory = Path.Combine(AzureEnvironment.GetTempPath(), "packages");

            var reposToUse = new List<IPackageRepository>
            {
                PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory)
            };

            if (!AzureEnvironment.IsRunningInCloud())
            {
                reposToUse.Add(MachineCache.Default);
            }

            reposToUse.AddRange(repositories.ToList().Select(r => PackageRepositoryFactory.Default.CreateRepository(r)));
            var repo = new AggregateRepository(reposToUse);

            packageManager = new PackageManager(repo, /*"packages"*/nugetCacheDirectory);
        }

        public IEnumerable<Target> DownloadAndExtractVersion(string version)
        {
            var semVer = SemanticVersion.Parse(version);

            var pkg = PackageRepositoryHelper.ResolvePackage(packageManager.SourceRepository, packageManager.LocalRepository, package, semVer, false);

            return
                from file in pkg.AssemblyReferences.OfType<PhysicalPackageAssemblyReference>()
                group file by file.TargetFramework
                into framework
                select new Target(framework.Key.FullName, framework.Select(f => f.SourcePath).ToList());
        }

        private readonly string package;
        private PackageManager packageManager;
    }

    public class Target
    {
        public Target(string name, List<string> files)
        {
            Name = name;
            Files = files;
        }

        public string Name { get; private set; }
        public List<string> Files { get; private set; }
    }
}