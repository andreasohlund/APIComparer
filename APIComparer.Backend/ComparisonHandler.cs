namespace APIComparer.Backend
{
    using Contracts;
    using NServiceBus;
    using NServiceBus.Logging;

    public class ComparisonHandler : IHandleMessages<CompareNugetPackage>
    {
        public void Handle(CompareNugetPackage message)
        {
            log.Info($"Received request to handle comparison for '{message.PackageId}' versions '{message.LeftVersion}' and '{message.RightVersion}'");

            var creator = new CompareSetCreator();
            var differ = new CompareSetDiffer();
            var reporter = new CompareSetReporter();

            var packageDescription = message.ToDescription();

            var compareSets = creator.Create(packageDescription);
            var diffedCompareSets = differ.Diff(compareSets);
            reporter.Report(packageDescription, diffedCompareSets);
        }

        static ILog log = LogManager.GetLogger<ComparisonHandler>();
    }
}