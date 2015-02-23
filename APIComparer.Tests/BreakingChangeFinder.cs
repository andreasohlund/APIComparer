using System.Collections.Generic;
using System.Linq;
using APIComparer;
using NuGet;

public static class BreakingChangeFinder
{
    public static IEnumerable<BreakingChange> Find(Diff diff)
    {
        IList<BreakingChange> breakingChanges = diff.RemovedPublicTypes().Select(t => new TypeRemoved(t))
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
            breakingChanges.AddRange(typeDiff.PublicFieldsRemoved().Select(d => new PublicFieldRemoved(typeDiff.LeftType, d)));
            //        typeDiff.PublicMethodsRemoved().Any() ||
            //typeDiff.FieldsChangedToNonPublic().Any() ||
            //typeDiff.MethodsChangedToNonPublic().Any()

        }

        return breakingChanges;
    }


}