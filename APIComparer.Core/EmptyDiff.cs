namespace APIComparer
{
    using System.Collections.Generic;
    using Mono.Cecil;

    public class EmptyDiff : Diff
    {
        public EmptyDiff()
        {
            LeftOrphanTypes = new List<TypeDefinition>();
            RightOrphanTypes = new List<TypeDefinition>();
            MatchingTypeDiffs = new List<TypeDiff>();
        }
    }
}