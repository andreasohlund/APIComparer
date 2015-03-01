namespace APIComparer.Tests.VersionComparisons
{
    using System.Collections.Generic;
    using System.Linq;
    using APIComparer.VersionComparisons;
    using NuGet;

    public class CompareLatestPatchStrategy : ICompareStrategy
    {
        public IEnumerable<VersionPair> GetVersionsToCompare(List<SemanticVersion> semverCompliantVersions)
        {

            var majorGroups = semverCompliantVersions.GroupBy(v => v.Version.Major);

            foreach (var major in majorGroups)
            {

                var minorGroups = major.GroupBy(v => v.Version.Minor);
                var left = minorGroups.First().Max();

                foreach (var minor in minorGroups)
                {
                    var right = minor.Max();

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