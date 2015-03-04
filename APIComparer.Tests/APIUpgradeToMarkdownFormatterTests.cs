using System;
using System.IO;
using System.Text;
using APIComparer;
using ApprovalTests;
using NuGet;
using NUnit.Framework;

[TestFixture]
public class APIUpgradeToMarkdownFormatterTests
{
    [Test]
    [Explicit]
    public void ApproveNServiceBus()
    {
        var packages = new[]
        {
            "4.6.4", "5.0.0-beta0004"
        };

        var nugetCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache");
        var repo = new AggregateRepository(new[]
        {
            PackageRepositoryFactory.Default.CreateRepository(nugetCacheDirectory),
            PackageRepositoryFactory.Default.CreateRepository("https://www.nuget.org/api/v2"),
            PackageRepositoryFactory.Default.CreateRepository("https://www.myget.org/F/particular/"),
        });

        var packageManager = new PackageManager(repo, "packages");

        foreach (var v in packages)
        {
            if (!Directory.Exists(Path.Combine("packages", "NServiceBus." + v)))
                packageManager.InstallPackage("NServiceBus", SemanticVersion.Parse(v));
        }

        var file1 = Path.Combine("packages", "NServiceBus." + packages[0], "lib", "net40", "NServiceBus.Core.dll");
        var file2 = Path.Combine("packages", "NServiceBus." + packages[1], "lib", "net45", "NServiceBus.Core.dll");

        var engine = new ComparerEngine();

        var diff = engine.CreateDiff(file1, file2);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder,"","" );

        formatter.WriteOut(diff);

        Approvals.Verify(stringBuilder.ToString());
    }

    [Test]
    public void ApproveExample()
    {
        var file1 = "ExampleV1.dll";
        var file2 = "ExampleV2.dll";

        var engine = new ComparerEngine();
        var diff = engine.CreateDiff(file1, file2);

        var stringBuilder = new StringBuilder();
        var formatter = new APIUpgradeToMarkdownFormatter(stringBuilder, "", "");

        formatter.WriteOut(diff);

        Approvals.Verify(stringBuilder.ToString());
    }
}