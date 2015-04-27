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
    [UseReporter(typeof(DiffReporter))]
    public class APIUpgradeToHtmlFormatterTest
    {
        [Test]
        public void TestCompliance()
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
    }
}