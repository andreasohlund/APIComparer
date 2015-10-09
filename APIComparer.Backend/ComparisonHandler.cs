namespace APIComparer.Backend
{
    using Contracts;
    using NServiceBus;

    public class ComparisonHandler : IHandleMessages<CompareNugetPackage>
    {
        public void Handle(CompareNugetPackage message)
        {
            var creator = new CompareSetCreator();
            var differ = new CompareSetDiffer();
            var reporter = new CompareSetReporter();

            var packageDescription = message.ToDescription();

            var compareSets = creator.Create(packageDescription);
            var diffedCompareSets = differ.Diff(compareSets);
            reporter.Report(packageDescription, diffedCompareSets);
        }
    }
}