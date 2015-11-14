namespace APIComparer.Tests.VersionComparisons
{
    using System.IO;
    using ApprovalTests;
    using ApprovalTests.Reporters;
    using APIComparer.Backend;
    using APIComparer.Backend.Reporting;
    using APIComparer.VersionComparisons;
    using NUnit.Framework;

    [TestFixture]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class APIUpgradeToHtmlFormatterTest
    {
        [Test]
        public void TestComplianceNewtonsoftJson()
        {
            Approvals.VerifyHtml(HtmlDiff("newtonsoft.json", "5.0.8", "6.0.8"));
        }

        [Test]
        public void TestComplianceNServiceBus()
        {
            Approvals.VerifyHtml(HtmlDiff("nservicebus", "5.0.0", "5.1.3"));
        }

        [Test]
        public void TestComplianceAzureSelfDestruct()
        {
            Approvals.VerifyHtml(HtmlDiff("Two10.Azure.SelfDestruct", "1.0.0", "1.0.5"));
        }

        [Test]
        public void TestComplianceLibLog()
        {
            Approvals.VerifyHtml(HtmlDiff("LibLog", "3.0.0", "4.1.1"));
        }

        [Test]
        public void AppccelerateEventBrokerNoChanges()
        {
            Approvals.VerifyHtml(HtmlDiff("Appccelerate.EventBroker", "3.1.0", "3.15.0"));
        }

        [Test]
        public void TestComplianceLibGit2SharpPackageEmbeddingNativeBinaries()
        {
            Approvals.VerifyHtml(HtmlDiff("LibGit2Sharp", "0.20.0", "0.20.1"));
        }

        [Test]
        public void TestComplianceLibGit2SharpNativeBinaries()
        {
            Approvals.VerifyHtml(HtmlDiff("LibGit2Sharp.NativeBinaries", "1.0.72", "1.0.81"));
        }

        [Test]
        public void TestComplianceLibGit2SharpNativeBinariesWithRightMissingNativeAssemblies()
        {
            Approvals.VerifyHtml(HtmlDiff("LibGit2Sharp", "0.21.0.176", "0.22.0-pre20150716071016"));
        }

        private string HtmlDiff(string packageName, string fromVersion, string untilVersion)
        {
            using (var writer = new StringWriter())
            {
                var formatter = new APIUpgradeToHtmlFormatter();

                var packageDescription = new PackageDescription
                {
                    PackageId = packageName,
                    Versions = new VersionPair(fromVersion, untilVersion)
                };

                var compareSetCreator = new CompareSetCreator();
                var sets = compareSetCreator.Create(packageDescription);
                var compareSetDiffer = new CompareSetDiffer();
                var diff = compareSetDiffer.Diff(sets);

                formatter.Render(writer, packageDescription, diff);

                return writer.ToString();
            }
        }

        [Test]
        public void Example()
        {
            var engine = new ComparerEngine();
            var diff = engine.CreateDiff("ExampleV1.dll", "ExampleV2.dll");

            var writer = new StringWriter();
            var formatter = new APIUpgradeToHtmlFormatter();
            var packageDescription = new PackageDescription
            {
                PackageId = "Example",
                Versions = new VersionPair("1.0.0", "2.0.0")
            };

            var diffedCompareSet = new DiffedCompareSet
            {
                Diff = diff,
                Set = new CompareSet()
            };

            formatter.Render(writer, packageDescription, new[]
            {
                diffedCompareSet
            });

            Approvals.VerifyHtml(writer.ToString());
        }
    }
}