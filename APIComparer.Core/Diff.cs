using System.Collections.Generic;
using Mono.Cecil;

namespace APIComparer
{
    public class Diff
    {
        public List<TypeDefinition> LeftOrphanTypes { get; set; }
        public List<TypeDefinition> RightOrphanTypes { get; set; }

        public List<TypeDiff> MatchingTypeDiffs { get; set; }
        
        public List<TypeDiff> MemberTypeDiffs { get; set; }
    }
}