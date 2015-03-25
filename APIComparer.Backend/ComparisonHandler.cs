namespace APIComparer.Backend
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using APIComparer.BreakingChanges;
    using APIComparer.Contracts;
    using APIComparer.VersionComparisons;
    using CommonMark;
    using NServiceBus;
    using NServiceBus.Logging;
    using NuGet;

    public class ComparisonHandler : IHandleMessages<CompareNugetPackage>
    {
        static readonly ILog Logger = LogManager.GetLogger<ComparisonHandler>();

        public IBus Bus { get; set; }

        public void Handle(CompareNugetPackage message)
        {
            var compareSets = GetNuGetVersionsToCompare(message);

            foreach (var set in compareSets)
            {
                Compare(set);
            }
        }

        static List<CompareSet> GetNuGetVersionsToCompare(CompareNugetPackage message)
        {
            return GetExplicitNuGetVersions(message.PackageId, message.LeftVersion, message.RightVersion).ToList();
        }

        private static IEnumerable<CompareSet> GetExplicitNuGetVersions(string nugetName, SemanticVersion leftVersion, SemanticVersion rightVersion)
        {
            yield return CreateCompareSet(nugetName, new VersionPair(leftVersion.ToString(), rightVersion.ToString()));
        }

        static CompareSet CreateCompareSet(string package, VersionPair versions)
        {
            var nugetDownloader = new NuGetDownloader(package, new List<string> { "https://www.nuget.org/api/v2" });

            Logger.DebugFormat("Preparing {0}-{1}", package, versions);

            var leftAssemblyGroup = new AssemblyGroup(nugetDownloader.DownloadAndExtractVersion(versions.LeftVersion));
            var rightAssemblyGroup = new AssemblyGroup(nugetDownloader.DownloadAndExtractVersion(versions.RightVersion));

            Logger.Debug(" done");

            return new CompareSet
            {
                Name = package,
                RightAssemblyGroup = rightAssemblyGroup,
                LeftAssemblyGroup = leftAssemblyGroup,
                Versions = versions
            };
        }

        static void Compare(CompareSet compareSet, bool showAllVersions = true)
        {
            var engine = new ComparerEngine();

            var diff = engine.CreateDiff(compareSet.LeftAssemblyGroup, compareSet.RightAssemblyGroup);

            var breakingChanges = BreakingChangeFinder.Find(diff)
                .ToList();

            if (showAllVersions || breakingChanges.Any())
            {
                Logger.DebugFormat("Checking {0}", compareSet);

                if (breakingChanges.Any())
                {
                    Logger.DebugFormat(": {0} Breaking Changes found", breakingChanges.Count());
                }
                else
                {
                    Logger.DebugFormat(" OK");
                }

                var resultFile = string.Format("{0}-{1}...{2}.md", compareSet.Name, compareSet.Versions.LeftVersion, compareSet.Versions.RightVersion);

                var rootPath = Environment.GetEnvironmentVariable("HOME");


                if (rootPath != null)
                {
                    rootPath = Path.Combine(rootPath, @".\site\wwwroot");
                }
                else
                {
                    rootPath = Environment.GetEnvironmentVariable("APICOMPARER_WWWROOT", EnvironmentVariableTarget.User);
                }

                if (string.IsNullOrEmpty(rootPath))
                {
                    throw new Exception("No root path could be found. If in development please set the `APICOMPARER_WWWROOT` env variable to the root folder of the webproject");
                }

                var directoryPath = Path.Combine(rootPath, "Comparisons");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var resultPath = Path.Combine(directoryPath, resultFile);

                using (var fileStream = File.OpenWrite(resultPath))
                using (var into = new StreamWriter(fileStream))
                {
                    //just write the markdown for now
                    var formatter = new APIUpgradeToMarkdownFormatter();
                    formatter.WriteOut(diff, into, new FormattingInfo("tbd", "tbd"));

                    into.Flush();
                    into.Close();
                    fileStream.Close();
                }

                using (var reader = new StreamReader(resultPath))
                using (var writer = new StreamWriter(Path.ChangeExtension(resultPath, "html")))
                {
                    CommonMarkConverter.Convert(reader, writer);

                    writer.Flush();
                    writer.Close();
                    reader.Close();
                }

                Logger.DebugFormat(", Full report written to " + resultFile);
            }
        }
    }
}