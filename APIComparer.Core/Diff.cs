using System.Collections.Generic;
using Mono.Cecil;

namespace APIComparer
{
    public class Diff
    {
        public AssemblyDefinition LeftAssembly { get; set; }
        public AssemblyDefinition RightAssembly { get; set; }

        public IList<TypeDefinition> LeftOrphanTypes { get; set; }
        public IList<TypeDefinition> RightOrphanTypes { get; set; }

        public IList<TypeDiff> MatchingTypeDiffs { get; set; }

        public IList<TypeDiff> MemberTypeDiffs { get; set; }
    }
}