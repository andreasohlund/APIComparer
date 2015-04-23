namespace APIComparer.Tests.VersionComparisons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ApprovalTests;
    using APIComparer.Backend;
    using NUnit.Framework;

    [TestFixture]
    public class CompareSetCreateTests
    {
        [Test]
        public void SingleMatchingTarget()
        {
            var compareSets = CompareSets.Create(new List<Target>{ new Target("net40",new List<string>())}, new List<Target>{ new Target("net40",new List<string>())});


            var output = string.Join(Environment.NewLine, compareSets.Select(cs=>cs.Name));

            Console.Out.WriteLine(output);
            Approvals.Verify(output);
        }
    }
}