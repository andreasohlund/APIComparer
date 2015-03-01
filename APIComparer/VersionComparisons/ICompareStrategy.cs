using System.Collections.Generic;
using NuGet;

namespace APIComparer.VersionComparisons
{
    interface ICompareStrategy
    {
        IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semverCompliantVersions);
    }
}