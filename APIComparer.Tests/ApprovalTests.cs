using System;
using System.IO;
using APIComparer.Outputters;
using ApprovalTests;
using NuGet;
using NUnit.Framework;

namespace APIComparer.Tests
{
    [TestFixture]
    public class ApprovalTests
    {
        [Test]
        public void ApproveMarkdown()
        {
            var packages = new[] { "4.6.3", "5.0.0-beta0002" };

            var repo = new AggregateRepository(new[] {
                PackageRepositoryFactory.Default.CreateRepository("https://www.nuget.org/api/v2"),
                PackageRepositoryFactory.Default.CreateRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NuGet", "Cache"))
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
            engine.Filter = new NServiceBusAPIFilter();

            var diff = engine.CreateDiff(file1, file2);

            var formatter = new APIUpgradeToMarkdownFormatter("Result.md");

            formatter.WriteOut(diff);

            Approvals.Verify(File.ReadAllText("Result.md"));
        }
    }
}