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

    public class ComparisonHandler : IHandleMessages<CompareNugetPackage>
    {
        static readonly ILog logger = LogManager.GetLogger<ComparisonHandler>();

        public IBus Bus { get; set; }

        public void Handle(CompareNugetPackage message)
        {
            var nugetDownloader = new NuGetDownloader(message.PackageId, new List<string> { "https://www.nuget.org/api/v2" });


            var leftTargets = nugetDownloader.DownloadAndExtractVersion(message.LeftVersion)
                .ToList();

            var rightTargets = nugetDownloader.DownloadAndExtractVersion(message.RightVersion)
                .ToList();
          


            var compareSets = CreateCompareSets(leftTargets, rightTargets)
                .ToList();

            Compare(message.PackageId, new VersionPair(message.LeftVersion, message.RightVersion), compareSets);
        }

        IEnumerable<CompareSet> CreateCompareSets(List<Target> leftTargets, List<Target> rightTargets)
        {
            //todo: smarter handling
            yield return new CompareSet
            {
                LeftAssemblyGroup = new AssemblyGroup(leftTargets.First().Files),
                RightAssemblyGroup = new AssemblyGroup(rightTargets.First().Files),
                Name = "net40"//todo               
            };
        }


        static void Compare(string packageId,VersionPair versions, List<CompareSet> compareSets)
        {
            var engine = new ComparerEngine();
            var formatter = new APIUpgradeToMarkdownFormatter();

            var resultPath = DetermineAndCreateResultPathIfNotExistant(packageId, versions);


            using (var fileStream = File.OpenWrite(resultPath))
            using (var into = new StreamWriter(fileStream))
            {
                foreach (var compareSet in compareSets)
                {

                    var diff = engine.CreateDiff(compareSet.LeftAssemblyGroup, compareSet.RightAssemblyGroup);

                    var breakingChanges = BreakingChangeFinder.Find(diff)
                        .ToList();

            
                    logger.DebugFormat("Checking {0}", compareSet);

                    if (breakingChanges.Any())
                    {
                        logger.DebugFormat(": {0} Breaking Changes found", breakingChanges.Count());
                    }
                    else
                    {
                        logger.DebugFormat(" OK");
                    }
                    into.WriteLine("# " + compareSet.Name);

                    formatter.WriteOut(diff, into, new FormattingInfo("tbd", "tbd"));
                }

                into.Flush();
                into.Close();
                fileStream.Close();
            }
            ConvertResultToHtmlAndRemoveTemporaryWorkFiles(resultPath);
        }

        static string DetermineAndCreateResultPathIfNotExistant(string packageId, VersionPair versions)
        {
            var resultFile = string.Format("{0}-{1}...{2}.md", packageId, versions.LeftVersion, versions.RightVersion);

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