using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using APIComparer;
using APIComparer.BreakingChanges;
using APIComparer.VersionComparisons;

class Program
{
    /* 
    Example cmd line
    .\APIComparer.exe    --source C:\Users\andreas.ohlund\Downloads\NServiceBus.5.2.0\NServiceBus.Core.dll --target C:\dev\NServiceBus\binaries\NServiceBus.Core.dll
    or
    .\APIComparer.exe --nuget NServiceBus.RabbitMQ --versions 1.0.1..1.1.5
         */
    static void Main(string[] args)
    {

        List<CompareSet> compareSets;

        if (args.Any(a => a == "--nuget"))
        {
            compareSets = GetNuGetVersionsToCompare(args);
        }
        else
        {
            compareSets = new List<CompareSet>
            {
                GetExplicitAssembliesToCompare(args)
            };
        }


        foreach (var set in compareSets)
        {
            Compare(set, args.All(a => a != "--show-failed-only"));
        }

        if (Debugger.IsAttached)
        {
            Console.ReadKey();
        }
    }

    private static void Compare(CompareSet compareSet,bool showAllVersions = true)
    {
        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(compareSet.LeftAssemblyGroup, compareSet.RightAssemblyGroup);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder, "tbd", "tbd");
        formatter.WriteOut(diff);


        var breakingChanges = BreakingChangeFinder.Find(diff)
            .ToList();

        if (showAllVersions || breakingChanges.Any())
        {
            Console.Out.Write("Checking {0}", compareSet);

            if (breakingChanges.Any())
            {
                Console.Out.Write(": {0} Breaking Changes found", breakingChanges.Count());
            }
            else
            {
                Console.Out.Write(" OK");
            }

            var resultFile = string.Format("{0}-{1}..{2}.md", compareSet.Name, compareSet.Versions.LeftVersion, compareSet.Versions.RightVersion);
            File.WriteAllText(resultFile, stringBuilder.ToString());

            Console.Out.WriteLine(", Full report written to " + resultFile);            
        }
    }

    static List<CompareSet> GetNuGetVersionsToCompare(string[] args)
    {
        var nugetIndex = Array.FindIndex(args, arg => arg == "--nuget");

        var package = args[nugetIndex + 1];

        var versionsIndex = Array.FindIndex(args, arg => arg == "--versions");


        if (versionsIndex < 0)
        {
            throw new Exception("No version range specified, please use --versions {source-version}..{target-version} or --version all");
        }

        var versions = args[versionsIndex + 1];

        if (versions.ToLower() == "all")
        {
            var compareStrategy = CompareStrategies.Default;

            var strategyIndex = Array.FindIndex(args, arg => arg == "--strategy");

            if (strategyIndex > 0)
            {
                compareStrategy = CompareStrategies.Parse(args[strategyIndex + 1]);
            }

            return GetAllNuGetVersions(package,compareStrategy).ToList();
        }
        else
        {
            return GetExplicitNuGetVersions(package, versions).ToList();
        }

    }

    static IEnumerable<CompareSet> GetAllNuGetVersions(string package, ICompareStrategy compareStrategy)
    {
        var browser = new NuGetBrowser();

        Console.Out.Write("Loading version history for {0}", package);
     
        var allVersions = browser.GetAllVersions(package);

        Console.Out.WriteLine(" - done");

        var semverCompliantVersions = allVersions.Where(v => v.Version.Major > 0)
            .ToList();



        return compareStrategy.GetVersionsToCompare(semverCompliantVersions)
            .Select(pair => CreateCompareSet(package, pair))
            .ToList();
    }

    private static IEnumerable<CompareSet> GetExplicitNuGetVersions(string nugetName, string versions)
    {
        var versionParts = versions.Split(new[] { ".." }, StringSplitOptions.None);

        var leftVersion = versionParts[0];

        var rightVersion = versionParts[1];
        yield return CreateCompareSet(nugetName, new VersionPair(leftVersion,rightVersion));
    }

    static CompareSet CreateCompareSet(string package, VersionPair versions)
    {
        var nugetDownloader = new NuGetDownloader(package);

        Console.Out.Write("Preparing {0}-{1}", package, versions);
     
        var leftAssemblyGroup = new AssemblyGroup(nugetDownloader.DownloadAndExtractVersion(versions.LeftVersion));
        var rightAssemblyGroup = new AssemblyGroup(nugetDownloader.DownloadAndExtractVersion(versions.RightVersion));

        Console.Out.WriteLine(" done");
     
        return new CompareSet
        {
            Name = package,
            RightAssemblyGroup = rightAssemblyGroup,
            LeftAssemblyGroup = leftAssemblyGroup,
            Versions = versions
        };
    }

    private static CompareSet GetExplicitAssembliesToCompare(string[] args)
    {
        var sourceIndex = Array.FindIndex(args, arg => arg == "--source");

        if (sourceIndex < 0)
        {
            throw new Exception("No target assemblies specified, please use --source {asm1};{asm2}...");
        }



        var targetIndex = Array.FindIndex(args, arg => arg == "--target");

        if (targetIndex < 0)
        {
            throw new Exception("No target assemblies specified, please use --target {asm1};{asm2}...");
        }

        return new CompareSet
        {
            Name = "Custom",
            RightAssemblyGroup = new AssemblyGroup(args[targetIndex + 1].Split(';').Select(Path.GetFullPath).ToList()),
            LeftAssemblyGroup = new AssemblyGroup(args[sourceIndex + 1].Split(';').Select(Path.GetFullPath).ToList()),
            Versions = new VersionPair("TBD-Left","TBD-Right")

        };

    }

}