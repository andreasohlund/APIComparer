using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using APIComparer;
using APIComparer.BreakingChanges;

class Program
{
    /*
     * 
Example cmd line --source C:\Users\andreas.ohlund\Downloads\NServiceBus.5.2.0\NServiceBus.Core.dll --target C:\dev\NServiceBus\binaries\NServiceBus.Core.dll
    
     * 
     */
    static void Main(string[] args)
    {

        CompareSet compareSet;

        if (args.Any(a => a == "--nuget"))
        {
            compareSet = GetNuGetVersionsToCompare(args);
        }
        else
        {
            compareSet = GetExplicitAssembliesToCompare(args);
        }

        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(compareSet.LeftAssemblyGroup, compareSet.RightAssemblyGroup);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder, "tbd", "tbd");
        formatter.WriteOut(diff);


        var breakingChanges = BreakingChangeFinder.Find(diff);

        Console.Out.Write("Checking {0}-{1}..{2}", compareSet.Name, compareSet.LeftVersion, compareSet.RightVersion);

        if (breakingChanges.Any())
        {
            Console.Out.WriteLine("--------  Breaking Changes found --------");

            foreach (var breakingChange in breakingChanges)
            {
                Console.Out.WriteLine(breakingChange.Reason);
            }

            Console.Out.WriteLine("-----------------------------------------");
            var resultFile = string.Format("{0}-{1}..{2}.md", compareSet.Name, compareSet.LeftVersion, compareSet.RightVersion);
            File.WriteAllText(resultFile, stringBuilder.ToString());

            Console.Out.WriteLine("Full report written to " + resultFile);
        }
        else
        {
            Console.Out.Write(" .... OK");
        }



        Console.ReadKey();
    }

    static CompareSet GetNuGetVersionsToCompare(string[] args)
    {
        var nugetIndex = Array.FindIndex(args, arg => arg == "--nuget");

        var nugetName = args[nugetIndex + 1];

        var versionsIndex = Array.FindIndex(args, arg => arg == "--versionrange");


        if (versionsIndex < 0)
        {
            throw new Exception("No version range specified, please use --versionrange {source-version}..{target-version}");
        }

        var versions = args[versionsIndex + 1].Split(new[] { ".." }, StringSplitOptions.None);

        var leftVersion = versions[0];

        var rightVersion = versions[1];
        var nugetDownloader = new NuGetDownloader(nugetName);


        return new CompareSet
        {
            Name = nugetName,
            RightAssemblyGroup = nugetDownloader.DownloadAndExtractVersion(rightVersion),
            LeftAssemblyGroup = nugetDownloader.DownloadAndExtractVersion(leftVersion),
            RightVersion = rightVersion,
            LeftVersion = leftVersion

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
            RightAssemblyGroup = args[targetIndex + 1].Split(';').Select(Path.GetFullPath).ToList(),
            LeftAssemblyGroup = args[sourceIndex + 1].Split(';').Select(Path.GetFullPath).ToList(),
            RightVersion = "TBD",
            LeftVersion = "TBD"

        };

    }

}

class CompareSet
{
    public List<string> LeftAssemblyGroup;
    public List<string> RightAssemblyGroup;

    public string LeftVersion;
    public string RightVersion;
    public string Name;



}