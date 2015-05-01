namespace APIComparer.Backend
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Versioning;
    using APIComparer.Shared;
    using NuGet;

    class NuGetDownloader
    {
        readonly string package;
        PackageManager packageManager;
        AggregateRepository repo;
        string nugetCacheDirectory;

        public NuGetDownloader(string nugetName, IEnumerable<string> repositories)
        {
            package = nugetName;

            nugetCacheDirectory = Path.Combine(AzureEnvironment.GetTempPath(), "packages");

            var reposToUse = new List<IPackageRepository>
            {
                PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory)
            };

            if (!AzureEnvironment.IsRunningInCloud())
            {
                reposToUse.Add(MachineCache.Default);
            }

            reposToUse.AddRange(repositories.ToList().Select(r => PackageRepositoryFactory.Default.CreateRepository(r)));
            repo = new AggregateRepository(reposToUse);

            packageManager = new PackageManager(repo, /*"packages"*/nugetCacheDirectory);
        }

        public IEnumerable<Target> DownloadAndExtractVersion(string version)
        {
            var semVer = SemanticVersion.Parse(version);

            IPackage pkg = PackageRepositoryHelper.ResolvePackage(packageManager.SourceRepository, packageManager.LocalRepository, package, semVer, false);

            return 
                from file in pkg.AssemblyReferences.OfType<PhysicalPackageAssemblyReference>()
                group file by file.TargetFramework
                into framework
                select new Target(framework.Key.FullName, framework.Select(f => f.SourcePath).ToList());
        }
    }

    public class Target
    {
        public string Name { get; private set; }
        public List<string> Files { get; private set; }

        public Target(string name, List<string> files)
        {
            Name = name;
            Files = files;
        }
    }
}