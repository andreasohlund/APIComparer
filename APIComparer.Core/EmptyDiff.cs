namespace APIComparer
{
    using System.Collections.Generic;
    using Mono.Cecil;

    public class EmptyDiff : Diff
    {
        public EmptyDiff()
        {
            RightAllTypes = new List<TypeDefinition>();
            LeftOrphanTypes = new List<TypeDefinition>();
            RightOrphanTypes = new List<TypeDefinition>();
            MatchingTypeDiffs = new List<TypeDiff>();
        }
    }
}