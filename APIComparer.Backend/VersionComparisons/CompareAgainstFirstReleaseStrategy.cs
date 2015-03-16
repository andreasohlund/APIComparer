using System.Collections.Generic;
using System.Linq;
using NuGet;

namespace APIComparer.VersionComparisons
{
    class CompareAgainstFirstReleaseStrategy: ICompareStrategy
    {
        public IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semverCompliantVersions)
        {
            var majorGroups = semverCompliantVersions.GroupBy(v => v.Version.Major);

            foreach (var major in majorGroups)
            {
                var firstRelease = major.First();

                foreach (var release in major)
                {
                    if (release == firstRelease)
                    {
                        continue;
                    }

                    yield return new VersionPair(firstRelease.Version.ToString(), release.Version.ToString());
                }
            }
        }
    }
}