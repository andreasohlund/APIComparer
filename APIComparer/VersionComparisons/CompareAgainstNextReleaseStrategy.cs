namespace APIComparer.VersionComparisons
{
    using System.Collections.Generic;
    using System.Linq;
    using NuGet;

    internal class CompareAgainstNextReleaseStrategy : ICompareStrategy
    {
        public IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semverCompliantVersions)
        {
            var majorGroups = semverCompliantVersions.GroupBy(v => v.Version.Major);

            foreach (var major in majorGroups)
            {
                var left = major.First();

                foreach (var right in major)
                {
                    if (right == left)
                    {
                        continue;
                    }

                    yield return new VersionPair(left.Version.ToString(), right.Version.ToString());

                    left = right;
                }
            }
        }
    }
}