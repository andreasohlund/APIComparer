namespace APIComparer.Tests.VersionComparisons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ApprovalTests;
    using ApprovalTests.Reporters;
    using APIComparer.Backend;
    using NUnit.Framework;

    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class CompareViewEngineTests
    {
        [Test]
        public void SingleMatchingTarget()
        {
            var engine = new CompareViewEngine();
            StringWriter writer = new StringWriter();

            var data = new
            {
                targets = new[]
                {
                    new
                    {
                        framework = "NET451",
                        foo = 3,
                    },
                    new
                    {
                        framework = "NET40",
                        foo = 2
                    }
                }
            };

            engine.Render(writer, data);

            Approvals.Verify(writer.ToString());
        }
    }
}