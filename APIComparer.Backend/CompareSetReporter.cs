namespace APIComparer.Backend
{
    using System;
    using System.IO;

    public class CompareSetReporter
    {
        public void Report(PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            var formatter = new APIUpgradeToMarkdownFormatter();

            var resultPath = DetermineAndCreateResultPathIfNotExistant(description);

            using (var fileStream = File.OpenWrite(resultPath))
            using (var into = new StreamWriter(fileStream))
            {
                foreach (var diffedCompareSet in diffedCompareSets)
                {
                    @into.WriteLine("# " + diffedCompareSet.Set.Name);

                    formatter.WriteOut(diffedCompareSet.Diff, @into, new FormattingInfo("tbd", "tbd"));
                }

                @into.Flush();
                @into.Close();
                fileStream.Close();
            }
            RemoveTemporaryWorkFiles(resultPath);
        }

        static string DetermineAndCreateResultPathIfNotExistant(PackageDescription description)
        {
            var resultFile = String.Format("{0}-{1}...{2}.md", description.PackageId, description.Versions.LeftVersion, description.Versions.RightVersion);

            var rootPath = Environment.GetEnvironmentVariable("HOME"); // TODO: use AzureEnvironment


            if (rootPath != null)
            {
                rootPath = Path.Combine(rootPath, @".\site\wwwroot");
            }
            else
            {
                rootPath = Environment.GetEnvironmentVariable("APICOMPARER_WWWROOT", EnvironmentVariableTarget.User);
            }

            if (String.IsNullOrEmpty(rootPath))
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

        static void RemoveTemporaryWorkFiles(string resultPath)
        {
            File.Delete(resultPath);
            File.Delete(Path.ChangeExtension(resultPath, ".running.html"));
        }
    }
}