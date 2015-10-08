using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace APIComparer
{
    public class Diff
    {
        public List<TypeDefinition> LeftOrphanTypes;
        public List<TypeDefinition> RightOrphanTypes;
        public List<TypeDiff> MatchingTypeDiffs;

        public IEnumerable<TypeDiff> TypesChangedToNonPublic()
        {
            return MatchingTypeDiffs.Where(x => !x.RightType.IsPublic && (x.LeftType.IsPublic && !x.LeftType.HasObsoleteAttribute()));
        }

        public IEnumerable<TypeDefinition> RemovedPublicTypes()
        {
            return LeftOrphanTypes.Where(x => x.IsPublic && !x.HasObsoleteAttribute());
        }
    }
}
