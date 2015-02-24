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

        var packageRoot = Path.Combine("packages", string.Format("{0}.{1}", nugetName, version), "lib");

        string dirPath = null;

        if (Directory.Exists(Path.Combine(packageRoot, "net45")))
        {
            dirPath = Path.Combine(packageRoot, "net45");
        }

        if (Directory.Exists(Path.Combine(packageRoot, "net40")))
        {
            dirPath = Path.Combine(packageRoot, "net40");
        }

        if (string.IsNullOrEmpty(dirPath))
        {
            throw new Exception("Couldn't find a lib/xyz dir for version " + version);
        }

        return Directory.EnumerateFiles(dirPath)
            .Where(f => f.EndsWith(".dll") || f.EndsWith(".exe"))
            .ToList();

    }
}