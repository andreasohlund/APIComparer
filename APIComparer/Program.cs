using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using APIComparer;
using NuGet;

class Program
{
    static void Main()
    {

        var nugetCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache");
        var repo = new AggregateRepository(new[]
        {
            PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory),
            PackageRepositoryFactory.Default.CreateRepository("https://www.nuget.org/api/v2"),
            PackageRepositoryFactory.Default.CreateRepository("https://www.myget.org/F/particular/"),
        });

        var packageManager = new PackageManager(repo, "packages");

        var newVersion = "5.0.0";
        packageManager.InstallPackage("NServiceBus", SemanticVersion.Parse(newVersion));
        packageManager.InstallPackage("NServiceBus.Host", SemanticVersion.Parse(newVersion));
        packageManager.InstallPackage("NServiceBus.Interfaces", SemanticVersion.Parse("4.6.6"));
        packageManager.InstallPackage("NServiceBus", SemanticVersion.Parse("4.6.6"));
        packageManager.InstallPackage("NServiceBus.Host", SemanticVersion.Parse("4.6.6"));

        var leftAssemblyGroup = new List<string>
        {
            Path.Combine("packages", "NServiceBus.4.6.6", "lib", "net40", "NServiceBus.Core.dll"),
            Path.Combine("packages", "NServiceBus.Interfaces.4.6.6", "lib", "net40", "NServiceBus.dll"),
            Path.Combine("packages", "NServiceBus.Host.4.6.6", "lib", "net40", "NServiceBus.Host.exe")
        };
        var rightAssemblyGroup = new List<string>
        {
            Path.Combine("packages", "NServiceBus." + newVersion, "lib", "net45", "NServiceBus.Core.dll"),
            Path.Combine("packages", "NServiceBus.Host." + newVersion, "lib", "net45", "NServiceBus.Host.exe"),
        };

        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(leftAssemblyGroup, rightAssemblyGroup);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder, "https://github.com/Particular/NServiceBus/blob/4.6.6/", "https://github.com/Particular/NServiceBus/tree/master/");
        formatter.WriteOut(diff);
        File.WriteAllText("Result.md", stringBuilder.ToString());

    }

}