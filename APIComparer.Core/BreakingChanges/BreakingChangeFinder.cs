using System.Collections.Generic;
using System.Linq;

namespace APIComparer.BreakingChanges
{
    public static class BreakingChangeFinder
    {
        public static IEnumerable<BreakingChange> Find(Diff diff)
        {
            List<BreakingChange> breakingChanges = diff.RemovedPublicTypes().Select(t => new TypeRemoved(t))
                .ToList<BreakingChange>();

            breakingChanges.AddRange(diff.TypesChangedToNonPublic()
                .Select(typeDiff => new TypeMadeNonPublic(typeDiff)));
            breakingChanges.AddRange(diff.TypesChangedToNonPublic()
                .Select(typeDiff => new TypeMadeNonPublic(typeDiff)));

            foreach (var typeDiff in diff.MatchingTypeDiffs)
            {
                if (typeDiff.LeftType.HasObsoleteAttribute())
                {
                    continue;
                }
                if (typeDiff.RightType.HasObsoleteAttribute())
                {
                    continue;
                }

                if (!typeDiff.LeftType.IsPublic)
                {
                    continue;
                }
                if (!typeDiff.RightType.IsPublic)
                {
                    continue;
                }
                breakingChanges.AddRange(typeDiff.PublicFieldsRemoved().Select(field => new PublicFieldRemoved(typeDiff.LeftType, field)));
                breakingChanges.AddRange(typeDiff.PublicMethodsRemoved().Select(method => new PublicMethodRemoved(typeDiff.LeftType, method)));
                breakingChanges.AddRange(typeDiff.FieldsChangedToNonPublic().Select(field => new FieldChangedToNonPublic(typeDiff.LeftType, field.Left)));
                breakingChanges.AddRange(typeDiff.MethodsChangedToNonPublic().Select(method => new MethodChangedToNonPublic(typeDiff.LeftType, method.Left)));
         
            }

            return breakingChanges;
        }


    }
}