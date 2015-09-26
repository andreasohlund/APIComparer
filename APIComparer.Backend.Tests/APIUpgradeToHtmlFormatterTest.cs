namespace APIComparer.Tests.VersionComparisons
{
    using System.IO;
    using ApprovalTests;
    using ApprovalTests.Reporters;
    using Backend;
    using Backend.Reporting;
    using APIComparer.VersionComparisons;
    using NUnit.Framework;

    [TestFixture]
    [UseReporter(typeof(DiffReporter),typeof(ClipboardReporter))]
    public class APIUpgradeToHtmlFormatterTest
    {
        [Test]
        public void TestComplianceNewtonsoftJson()
        {
            var formatter = new APIUpgradeToHtmlFormatter();
            var writer = new StringWriter();
            var packageDescription = new PackageDescription
            {
                PackageId = "newtonsoft.json",
                Versions = new VersionPair("5.0.8", "6.0.8")
            };
            var compareSetCreator = new CompareSetCreator();
            var sets = compareSetCreator.Create(packageDescription);
            var compareSetDiffer = new CompareSetDiffer();
            var diff = compareSetDiffer.Diff(sets);

            formatter.Render(writer, packageDescription, diff);

            Approvals.VerifyHtml(writer.ToString());
        }

        [Test]
        public void TestComplianceNServiceBus()
        {
            var formatter = new APIUpgradeToHtmlFormatter();
            var writer = new StringWriter();
            var packageDescription = new PackageDescription
            {
                PackageId = "nservicebus",
                Versions = new VersionPair("4.0.0", "5.0.0")
            };
            var compareSetCreator = new CompareSetCreator();
            var sets = compareSetCreator.Create(packageDescription);
            var compareSetDiffer = new CompareSetDiffer();
            var diff = compareSetDiffer.Diff(sets);

            formatter.Render(writer, packageDescription, diff);

            Approvals.VerifyHtml(writer.ToString());
        }

        [Test]
        public void TestComplianceAzureSelfDestruct()
        {
            var formatter = new APIUpgradeToHtmlFormatter();
            var writer = new StringWriter();
            var packageDescription = new PackageDescription
            {
                PackageId = "Two10.Azure.SelfDestruct",
                Versions = new VersionPair("1.0.0", "1.0.5")
            };
            var compareSetCreator = new CompareSetCreator();
            var sets = compareSetCreator.Create(packageDescription);
            var compareSetDiffer = new CompareSetDiffer();
            var diff = compareSetDiffer.Diff(sets);

            formatter.Render(writer, packageDescription, diff);

            Approvals.VerifyHtml(writer.ToString());
        }

        [Test]
        public void TestComplianceLibLog()
        {
            var formatter = new APIUpgradeToHtmlFormatter();
            var writer = new StringWriter();
            var packageDescription = new PackageDescription
            {
                PackageId = "LibLog",
                Versions = new VersionPair("3.0.0", "4.1.1")
            };
            var compareSetCreator = new CompareSetCreator();
            var sets = compareSetCreator.Create(packageDescription);
            var compareSetDiffer = new CompareSetDiffer();
            var diff = compareSetDiffer.Diff(sets);

            formatter.Render(writer, packageDescription, diff);

            Approvals.VerifyHtml(writer.ToString());
        }

        [Test]
        public void AppccelerateEventBrokerNoChanges()
        {
            var formatter = new APIUpgradeToHtmlFormatter();
            var writer = new StringWriter();
            var packageDescription = new PackageDescription
            {
                PackageId = "Appccelerate.EventBroker",
                Versions = new VersionPair("3.1.0", "3.15.0")
            };
            var compareSetCreator = new CompareSetCreator();
            var sets = compareSetCreator.Create(packageDescription);
            var compareSetDiffer = new CompareSetDiffer();
            var diff = compareSetDiffer.Diff(sets);

            formatter.Render(writer, packageDescription, diff);

            Approvals.VerifyHtml(writer.ToString());
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
            
            formatter.Render(writer, packageDescription, new [] {diffedCompareSet});

            Approvals.VerifyHtml(writer.ToString());
        }

    }
}