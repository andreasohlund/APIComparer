namespace APIComparer.Backend
{
    using System.Collections.Generic;
    using System.Linq;
    using VersionComparisons;

    public class CompareSetCreator
    {
        public CompareSet[] Create(PackageDescription description)
        {
            var nugetDownloader = new NuGetDownloader(description.PackageId, new List<string>
            {
                "https://www.nuget.org/api/v2"
            });

            var leftTargets = nugetDownloader.DownloadAndExtractVersion(description.Versions.LeftVersion)
                .ToList();

            var rightTargets = nugetDownloader.DownloadAndExtractVersion(description.Versions.RightVersion)
                .ToList();

            return CompareSets.Create(leftTargets, rightTargets).ToArray();
        }
    }
}