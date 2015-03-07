namespace APIComparer.Backend
{
    using APIComparer.Contracts;
    using NServiceBus;

    public class ComparisonHandler : IHandleMessages<CompareNugetPackage>
    {
        public IBus Bus { get; set; }

        public void Handle(CompareNugetPackage message)
        {
        }
    }
}