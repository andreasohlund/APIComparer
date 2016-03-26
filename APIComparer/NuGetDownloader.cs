using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet;

class NuGetDownloader
{
    public NuGetDownloader(string nugetName, IEnumerable<string> repositories)
    {
        package = nugetName;

        var nugetCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache");


        var reposToUse = new List<IPackageRepository>
        {
            PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory)
        };

        reposToUse.AddRange(repositories.ToList().Select(r => PackageRepositoryFactory.Default.CreateRepository(r)));
        var repo = new AggregateRepository(reposToUse);

        packageManager = new PackageManager(repo, "packages");
    }

    public List<string> DownloadAndExtractVersion(string version)
    {
        var semVer = SemanticVersion.Parse(version);

        packageManager.InstallPackage(package, semVer, true, false);


        var dirPath = Path.Combine("packages", $"{package}.{version}", "lib");


        if (Directory.Exists(Path.Combine(dirPath, "net20")))
        {
            dirPath = Path.Combine(dirPath, "net20");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net30")))
        {
            dirPath = Path.Combine(dirPath, "net30");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net35")))
        {
            dirPath = Path.Combine(dirPath, "net35");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net40")))
        {
            dirPath = Path.Combine(dirPath, "net40");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net45")))
        {
            dirPath = Path.Combine(dirPath, "net45");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net451")))
        {
            dirPath = Path.Combine(dirPath, "net451");
        }

        if (Directory.Exists(Path.Combine(dirPath, "net452")))
        {
            dirPath = Path.Combine(dirPath, "net452");
        }

        var files = Directory.EnumerateFiles(dirPath)
            .Where(f => f.EndsWith(".dll") || f.EndsWith(".exe"))
            .ToList();


        if (!files.Any())
        {
            throw new Exception("Couldn't find any assemblies in  " + dirPath);
        }

        return files;
    }

    readonly string package;
    PackageManager packageManager;
}