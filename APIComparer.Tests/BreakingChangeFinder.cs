using System.Collections.Generic;
using System.Linq;
using APIComparer;

public static class BreakingChangeFinder
{
    public static IEnumerable<BreakingChange> Find(Diff diff)
    {
        var result = diff.RemovedPublicTypes().Select(t => new BreakingChange(t));

        return result;
    }


}