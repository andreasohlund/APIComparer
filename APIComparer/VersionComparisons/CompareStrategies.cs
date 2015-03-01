using System;

namespace APIComparer.VersionComparisons
{
    class CompareStrategies
    {
        public static ICompareStrategy Default
        {
            get { return new CompareAgainstFirstReleaseStrategy(); }
        }

        public static ICompareStrategy Parse(string strategy)
        {
            if (strategy == "next-release")
            {
                return new CompareAgainstNextReleaseStrategy();
            }

            throw new Exception("Unknown strategy " + strategy);
        }
    }
}