namespace APIComparer.Tests.VersionComparisons
{
    using System;
    using System.Collections.Generic;
    using ApprovalTests;
    using NuGet;
    using NUnit.Framework;

    [TestFixture]
    public class CompareLatestPatchStrategyTests
    {
        [Test]
        public void Run()
        {
            var allVersions = new List<SemanticVersion>
            {
                new SemanticVersion("1.0.0"),
                new SemanticVersion("1.0.1"),
                new SemanticVersion("1.1.0"),
                new SemanticVersion("1.2.0"),
                new SemanticVersion("1.2.1"),
                new SemanticVersion("1.2.3"),
                new SemanticVersion("2.0.0"),
                new SemanticVersion("2.1.0")
            };

            var versions = new CompareLatestPatchStrategy()
                .GetVersionsToCompare(allVersions);


            var output = string.Join(Environment.NewLine, versions);

            Console.Out.WriteLine(output);
            Approvals.Verify(output);
        }
    }
}