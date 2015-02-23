using System.Collections.Generic;
using System.Linq;
using APIComparer;
using NuGet;

public static class BreakingChangeFinder
{
    public static IEnumerable<BreakingChange> Find(Diff diff)
    {
        IList<BreakingChange> result = diff.RemovedPublicTypes().Select(t=>new TypeRemoved(t))
            .ToList<BreakingChange>();

        result.AddRange(diff.TypesChangedToNonPublic()
            .Select(typeDiff=>new TypeMadeNonPublic(typeDiff)));

        return result;
    }


}