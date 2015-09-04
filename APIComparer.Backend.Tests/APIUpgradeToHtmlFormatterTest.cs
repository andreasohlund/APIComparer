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
    [UseReporter(typeof(DiffReporter),typeof(ClipboardReporter))]
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
            Approvals.VerifyHtml(HtmlDiff("nservicebus", "4.0.0", "5.0.0"));
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
    }
}
