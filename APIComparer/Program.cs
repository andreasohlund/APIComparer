using System;
using System.IO;
using System.Linq;
using System.Text;
using APIComparer;
using APIComparer.BreakingChanges;

class Program
{
    static void Main(string[] args)
    {

        //var nugetCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache");
        //var repo = new AggregateRepository(new[]
        //{
        //    PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory),
        //    PackageRepositoryFactory.Default.CreateRepository("https://www.nuget.org/api/v2"),
        //    PackageRepositoryFactory.Default.CreateRepository("https://www.myget.org/F/particular/"),
        //});

        //var packageManager = new PackageManager(repo, "packages");

        //var newVersion = "5.0.0";
        //packageManager.InstallPackage("NServiceBus", SemanticVersion.Parse(newVersion));
        //packageManager.InstallPackage("NServiceBus.Host", SemanticVersion.Parse(newVersion));
        //packageManager.InstallPackage("NServiceBus.Interfaces", SemanticVersion.Parse("4.6.7"));
        //packageManager.InstallPackage("NServiceBus", SemanticVersion.Parse("4.6.7"));
        //packageManager.InstallPackage("NServiceBus.Host", SemanticVersion.Parse("4.6.7"));


        var sourceIndex = Array.FindIndex(args, arg => arg == "--source");

        if (sourceIndex < 0)
        {
            throw new Exception("No target assemblies specified, please use --source {asm1};{asm2}...");
        }

        var leftAssemblyGroup = args[sourceIndex + 1].Split(';').Select(Path.GetFullPath).ToList();


        var targetIndex = Array.FindIndex(args, arg => arg == "--target");

        if (targetIndex < 0)
        {
            throw new Exception("No target assemblies specified, please use --target {asm1};{asm2}...");
        }

        var rightAssemblyGroup = args[targetIndex + 1].Split(';').Select(Path.GetFullPath).ToList();

        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(leftAssemblyGroup, rightAssemblyGroup);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder, "https://github.com/Particular/NServiceBus/blob/5.2.0/", "https://github.com/Particular/NServiceBus/blob/develop/");
        formatter.WriteOut(diff);
        File.WriteAllText("Result.md", stringBuilder.ToString());

        var breakingChanges = BreakingChangeFinder.Find(diff);

        if (breakingChanges.Any())
        {
            Console.Out.WriteLine("--------  Breaking Changes found --------");

            foreach (var breakingChange in breakingChanges)
            {
                Console.Out.WriteLine(breakingChange.Reason);
            }
        
            Console.Out.WriteLine("-----------------------------------------");
        }


        Console.Out.WriteLine("Full report written to Result.md");

        Console.ReadKey();
    }

}