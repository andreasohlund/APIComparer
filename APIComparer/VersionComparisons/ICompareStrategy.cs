using System.Collections.Generic;
using NuGet;

namespace APIComparer.VersionComparisons
{
    public interface ICompareStrategy
    {
        IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semVerCompliantVersions);
    }
}