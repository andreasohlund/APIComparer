using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet;

class NuGetDownloader
{
    readonly string package;
    PackageManager packageManager;

    public NuGetDownloader(string nugetName)
    {
        package = nugetName;

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
        var semVer = SemanticVersion.Parse(version);

        packageManager.InstallPackage(package,semVer,true,false);
     
        //handle package revs like 1.0.0.1
        var trimmedVersion = string.Format("{0}.{1}.{2}", semVer.Version.Major, semVer.Version.Minor, semVer.Version.Build);

        if (semVer.Version.Revision > 0)
        {
            trimmedVersion += "." + semVer.Version.Revision;
        }
        
        var dirPath = Path.Combine("packages", string.Format("{0}.{1}", package, trimmedVersion), "lib");


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

class NuGetBrowser
{
    IPackageRepository repository;

    public NuGetBrowser()
    {
        repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
    }

    public IList<SemanticVersion> GetAllVersions(string package)
    {
     
        var packages = repository.FindPackagesById(package).ToList();

        return  packages.Where(item => item.IsReleaseVersion() && item.IsListed())
            .Select(p=>p.Version)
            .ToList();

    }
}