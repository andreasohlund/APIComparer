using System;
using ApprovalTests;
using APIComparer;
using APIComparer.BreakingChanges;
using NUnit.Framework;

[TestFixture]
class BreakingChangeFinderTests
{

    [Test]
    public void ApproveExample()
    {
        var file1 = "ExampleV1.dll";
        var file2 = "ExampleV2.dll";

        var engine = new ComparerEngine();
        var diff = engine.CreateDiff(file1, file2);

        var breakingChanges = BreakingChangeFinder.Find(diff);
        Approvals.Verify(string.Join(Environment.NewLine,breakingChanges));
    }
}