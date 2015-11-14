using System;
using ApprovalTests;
using APIComparer;
using APIComparer.BreakingChanges;
using NUnit.Framework;

[TestFixture]
internal class BreakingChangeFinderTests
{
    [Test]
    public void ApproveExample()
    {
        var engine = new ComparerEngine();
        var diff = engine.CreateDiff("ExampleV1.dll", "ExampleV2.dll");

        var breakingChanges = BreakingChangeFinder.Find(diff);
        Approvals.Verify(string.Join(Environment.NewLine, breakingChanges));
    }
}