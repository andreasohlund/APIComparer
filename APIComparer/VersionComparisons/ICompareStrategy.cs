namespace APIComparer.VersionComparisons
{
    using System.Collections.Generic;
    using NuGet;

    public interface ICompareStrategy
    {
        IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semVerCompliantVersions);
    }
}