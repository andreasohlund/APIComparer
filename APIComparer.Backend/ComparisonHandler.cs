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
        static readonly ILog logger = LogManager.GetLogger<ComparisonHandler>();

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
            return GetExplicitNuGetVersions(message.PackageId, message.LeftVersion, message.RightVersion, message.Target).ToList();
        }

        private static IEnumerable<CompareSet> GetExplicitNuGetVersions(string nugetName, SemanticVersion leftVersion, SemanticVersion rightVersion, string target)
        {
            yield return CreateCompareSet(nugetName, new VersionPair(leftVersion.ToString(), rightVersion.ToString()), target);
        }

        static CompareSet CreateCompareSet(string package, VersionPair versions, string target)
        {
            var nugetDownloader = new NuGetDownloader(package, new List<string> { "https://www.nuget.org/api/v2" });

            logger.DebugFormat("Preparing {0}-{1}", package, versions);

            var leftVersionAssemblies = nugetDownloader.DownloadAndExtractVersion(versions.LeftVersion, target);
            var rightVersionAssemblies = nugetDownloader.DownloadAndExtractVersion(versions.RightVersion, target);

            var leftAssemblyGroup = leftVersionAssemblies.Any() ? new AssemblyGroup(leftVersionAssemblies) : new EmptyAssemblyGroup();
            var rightAssemblyGroup = rightVersionAssemblies.Any() ? new AssemblyGroup(rightVersionAssemblies) : new EmptyAssemblyGroup();

            logger.DebugFormat("Done with {0}-{1}", package, versions);

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

            var resultPath = DetermineAndCreateResultPathIfNotExistant(compareSet);

            if (showAllVersions || breakingChanges.Any())
            {
                logger.DebugFormat("Checking {0}", compareSet);

                if (breakingChanges.Any())
                {
                    logger.DebugFormat(": {0} Breaking Changes found", breakingChanges.Count());
                }
                else
                {
                    logger.DebugFormat(" OK");
                }

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
            }
            else
            {
                using (var fileStream = File.OpenWrite(resultPath))
                using (var into = new StreamWriter(fileStream))
                {
                    into.Write("The comparison didn't find anything to compare against."); // TODO: Add detailed reason why this can happen

                    into.Flush();
                    into.Close();
                    fileStream.Close();
                }
            }
            ConvertResultToHtmlAndRemoveTemporaryWorkFiles(resultPath);
        }

        static string DetermineAndCreateResultPathIfNotExistant(CompareSet compareSet)
        {
            var resultFile = string.Format("{0}-{1}...{2}.md", compareSet.Name, compareSet.Versions.LeftVersion, compareSet.Versions.RightVersion);

            var rootPath = Environment.GetEnvironmentVariable("HOME"); // TODO: use AzureEnvironment


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
            return resultPath;
        }

        static void ConvertResultToHtmlAndRemoveTemporaryWorkFiles(string resultPath)
        {
            using (var reader = new StreamReader(resultPath))
            {
                using (var writer = new StreamWriter(Path.ChangeExtension(resultPath, "html")))
                {
                    CommonMarkConverter.Convert(reader, writer);

                    writer.Flush();
                    writer.Close();
                    reader.Close();
                }
            }

            File.Delete(resultPath);
            File.Delete(Path.ChangeExtension(resultPath, ".running.html"));
            logger.DebugFormat("Full report written to {0}", resultPath);
        }
    }
}