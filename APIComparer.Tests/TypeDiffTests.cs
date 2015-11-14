using System.Collections.Generic;
using System.Linq;
using APIComparer;
using Mono.Cecil;
using NUnit.Framework;

[TestFixture]
internal class TypeDiffTests
{
    [Test]
    public void VerifyMissingFieldDueToVisibility()
    {
        var leftType = new TypeDefinition("", "TheType", TypeAttributes.Public);
        leftType.Fields.Add(new FieldDefinition("TheField", FieldAttributes.Public, GetObjectType()));
        var left = new List<TypeDefinition>
        {
            leftType
        };
        var rightType = new TypeDefinition("", "TheType", TypeAttributes.Public);
        rightType.Fields.Add(new FieldDefinition("TheField", FieldAttributes.Private, GetObjectType()));
        var right = new List<TypeDefinition>
        {
            rightType
        };
        var diff = new ComparerEngine().CreateDiff(left, right);
        Assert.AreEqual(1, diff.MatchingTypeDiffs.First().MatchingFields.Count);
    }

    public TypeReference GetObjectType()
    {
        return new TypeReference("System", "Object", null, null, false);
    }
}