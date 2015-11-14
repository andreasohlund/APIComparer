namespace APIComparer.VersionComparisons
{
    using System;

    internal class CompareStrategies
    {
        public static ICompareStrategy Default
        {
            get { return new CompareLatestPatchStrategy(); }
        }

        public static ICompareStrategy Parse(string strategy)
        {
            if (strategy == "next-release")
            {
                return new CompareAgainstNextReleaseStrategy();
            }

            if (strategy == "first-release")
            {
                return new CompareAgainstNextReleaseStrategy();
            }

            throw new Exception("Unknown strategy " + strategy);
        }
    }
}