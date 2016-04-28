using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using APIComparer;
using APIComparer.BreakingChanges;
using APIComparer.VersionComparisons;

static class Program
{
    /* 
    Example cmd line
    .\APIComparer.exe    --source C:\Users\andreas.ohlund\Downloads\NServiceBus.5.2.0\NServiceBus.Core.dll --target C:\dev\NServiceBus\binaries\NServiceBus.Core.dll
    or
    .\APIComparer.exe --nuget NServiceBus.RabbitMQ --versions 1.0.1..1.1.5
     
     * comparing local bin to latest build: --target C:\dev\NServiceBus\binaries\NServiceBus.Core.dll --source nuget:NServiceBus --feeds myget:particular --include-prerelease
         */
    static int Main(string[] args)
    {
        try
        {
            List<CompareSet> compareSets;

            if (args.Contains("--nuget"))
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
            var showAllVersions = !args.Contains("--show-failed-only");
            var nonInteractive = args.Contains("--non-interactive");
            var reportPath = GetReportPath(args);

            var noBreakingChanges = compareSets.All(set => Compare(set, showAllVersions, nonInteractive, reportPath));

            if (Debugger.IsAttached)
            {
                Console.ReadKey();
            }

            return noBreakingChanges
                ? ExitCode.Ok
                : ExitCode.BreakingChange;
        }
        catch (ApiComparerArgumentException ex)
        {
            Console.Error.WriteLine("Argument exception: {0}", ex.Message);
            return ExitCode.InvalidArgument;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unhandled exception: {0}", ex);
            return ExitCode.GenericError;
        }
    }

    static bool Compare(CompareSet compareSet, bool showAllVersions = true, bool nonInteractive = false, string reportPath = null)
    {
        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(compareSet.LeftAssemblyGroup, compareSet.RightAssemblyGroup);

        var breakingChanges = BreakingChangeFinder.Find(diff)
            .ToList();

        var hasBreakingChange = breakingChanges.Any();

        if (!showAllVersions && !hasBreakingChange)
        {
            return true;
        }

        Console.Out.Write("Checking {0}", compareSet);

        if (hasBreakingChange)
        {
            Console.Out.WriteLine(": {0} Breaking Changes found", breakingChanges.Count);
        }
        else
        {
            Console.Out.WriteLine(": No breaking changes found");
        }

        var resultFile = reportPath ?? Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".md");

        using (var fileStream = File.OpenWrite(resultFile))
        using (var into = new StreamWriter(fileStream))
        {
            var formatter = new APIUpgradeToMarkdownFormatter();
            formatter.WriteOut(ApiChanges.FromDiff(diff), @into);

            @into.Flush();
            @into.Close();
            fileStream.Close();
        }

        Console.Out.WriteLine(resultFile);
        if (!nonInteractive)
        {
            Process.Start(resultFile);
        }
        return !hasBreakingChange;
    }

    static List<CompareSet> GetNuGetVersionsToCompare(string[] args)
    {
        var nugetIndex = Array.FindIndex(args, arg => arg == "--nuget");

        var package = args[nugetIndex + 1];

        var versionsIndex = Array.FindIndex(args, arg => arg == "--versions");


        if (versionsIndex < 0)
        {
            throw new ApiComparerArgumentException("No version range specified, please use --versions {source-version}..{target-version} or --version all");
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

            return GetAllNuGetVersions(package, compareStrategy).ToList();
        }

        return GetExplicitNuGetVersions(package, versions).ToList();
    }

    static IEnumerable<CompareSet> GetAllNuGetVersions(string package, ICompareStrategy compareStrategy)
    {
        var browser = new NuGetBrowser(new List<string> { "https://www.nuget.org/api/v2" });

        Console.Out.Write("Loading version history for {0}", package);

        var allVersions = browser.GetAllVersions(package);

        Console.Out.WriteLine(" - done");

        var semverCompliantVersions = allVersions.Where(v => v.Version.Major > 0)
            .ToList();



        return compareStrategy.GetVersionsToCompare(semverCompliantVersions)
            .Select(pair => CreateCompareSet(package, pair))
            .ToList();
    }

    static IEnumerable<CompareSet> GetExplicitNuGetVersions(string nugetName, string versions)
    {
        var versionParts = versions.Split(new[] { ".." }, StringSplitOptions.None);

        var leftVersion = versionParts[0];

        var rightVersion = versionParts[1];
        yield return CreateCompareSet(nugetName, new VersionPair(leftVersion, rightVersion));
    }

    static CompareSet CreateCompareSet(string package, VersionPair versions)
    {
        var nugetDownloader = new NuGetDownloader(package, new List<string> { "https://www.nuget.org/api/v2" });

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

    static CompareSet GetExplicitAssembliesToCompare(string[] args)
    {
        var sourceIndex = Array.FindIndex(args, arg => arg == "--source");

        if (sourceIndex < 0)
        {
            throw new ApiComparerArgumentException("No target assemblies specified, please use --source {asm1};{asm2}...");
        }

        var targetIndex = Array.FindIndex(args, arg => arg == "--target");

        if (targetIndex < 0)
        {
            throw new ApiComparerArgumentException("No target assemblies specified, please use --target {asm1};{asm2}...");
        }

        var source = args[sourceIndex + 1];

        AssemblyGroup leftAsmGroup;

        string leftVersion;

        var compareName = "Custom";

        if (source.StartsWith("nuget:"))
        {

            var nugetName = source.Replace("nuget:", "").Trim();


            compareName = nugetName;

            var feeds = GetFeedsToUse(args);

            var nugetBrowser = new NuGetBrowser(feeds);

            var version = nugetBrowser.GetAllVersions(nugetName, args.Contains("--include-prerelease")).Max();

            var nugetDownloader = new NuGetDownloader(nugetName, feeds);

            leftVersion = version.ToString();

            leftAsmGroup = new AssemblyGroup(nugetDownloader.DownloadAndExtractVersion(leftVersion));
        }
        else
        {
            leftVersion = "TBD-Left";

            leftAsmGroup = new AssemblyGroup(source.Split(';').Select(Path.GetFullPath).ToList());
        }

        return new CompareSet
        {
            Name = compareName,
            RightAssemblyGroup = new AssemblyGroup(args[targetIndex + 1].Split(';').Select(Path.GetFullPath).ToList()),
            LeftAssemblyGroup = leftAsmGroup,
            Versions = new VersionPair(leftVersion, "TBD-Right")

        };

    }

    static List<string> GetFeedsToUse(string[] args)
    {
        var feedsIndex = Array.FindIndex(args, arg => arg == "--feeds");

        if (feedsIndex < 0)
        {
            return new List<string> { "https://www.nuget.org/api/v2" };
        }

        var feeds = new List<string>();
        var parts = args[feedsIndex + 1].Split(';').ToList();

        foreach (var feed in parts)
        {
            if (feed.ToLower().StartsWith("myget:"))
            {
                var mygetFeedName = feed.Split(':')[1];

                feeds.Add($"https://www.myget.org/F/{mygetFeedName}/api/v2");
            }
            else
            {
                feeds.Add(feed);
            }
        }

        return feeds;
    }

    static string GetReportPath(IEnumerable<string> args)
    {
        var reportPathSpecified = false;
        var reportPath = args.SkipWhile(arg => arg != "--report-path")
            .Where(arg=>reportPathSpecified = true)
            .Skip(1)
            .Take(1)
            .FirstOrDefault(arg => !string.IsNullOrWhiteSpace(arg) && !arg.StartsWith("--"));

        if (!string.IsNullOrWhiteSpace(reportPath))
        {
            using (File.Create(reportPath))
            {
            }
        }
        else if (reportPathSpecified)
        {
            throw new ApiComparerArgumentException("--report-path is specified, but no valid file path, please use --report-path {report path}");
        }
        return reportPath;
    }
}