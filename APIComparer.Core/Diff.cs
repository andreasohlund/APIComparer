namespace APIComparer
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;

    public class Diff
    {
        public IEnumerable<TypeDiff> TypesChangedToNonPublic()
        {
            return MatchingTypeDiffs.Where(x => !x.RightType.IsPublic && x.LeftType.IsPublic && !x.LeftType.HasObsoleteAttribute());
        }

        public IEnumerable<TypeDefinition> RemovedPublicTypes()
        {
            return LeftOrphanTypes.Where(x => x.IsPublic && !x.HasObsoleteAttribute());
        }

        public List<TypeDefinition> LeftOrphanTypes;
        public List<TypeDiff> MatchingTypeDiffs;
        public List<TypeDefinition> RightOrphanTypes;
    }
}