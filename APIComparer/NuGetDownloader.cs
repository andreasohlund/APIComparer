using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet;

class NuGetDownloader
{
    readonly string nugetName;
    PackageManager packageManager;

    public NuGetDownloader(string nugetName)
    {
        this.nugetName = nugetName;

        var nugetCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache");
        var repo = new AggregateRepository(new[]
        {
            PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory),
            PackageRepositoryFactory.Default.CreateRepository("https://www.nuget.org/api/v2"),
            //PackageRepositoryFactory.Default.CreateRepository("https://www.myget.org/F/particular/"),
        });

        packageManager = new PackageManager(repo, "packages");
    }

    public List<string> DownloadAndExtractVersion(string version)
    {
        packageManager.InstallPackage(nugetName, SemanticVersion.Parse(version));

        var dirPath = Path.Combine("packages", string.Format("{0}.{1}", nugetName, version), "lib");


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

        var files = Directory.EnumerateFiles(dirPath)
            .Where(f => f.EndsWith(".dll") || f.EndsWith(".exe"))
            .ToList();


        if (!files.Any())
        {
            throw new Exception("Couldn't find any assemblies in  " + dirPath);
        }

        return files;
    }
}