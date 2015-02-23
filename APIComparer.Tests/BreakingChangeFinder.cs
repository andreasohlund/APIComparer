using System.Collections.Generic;
using System.Linq;
using APIComparer;

public static class BreakingChangeFinder
{
    public static IEnumerable<BreakingChange> Find(Diff diff)
    {
        var result = diff.RemovedPublicTypes().Select(BreakingChange.TypeRemoved)
            .ToList();

        result.AddRange(diff.TypesChangedToNonPublic()
            .Select(BreakingChange.TypeMadeNonPublic));

        return result;
    }


}