namespace APIComparer.Contracts
{
    using System;
    using NServiceBus;

    public class CompareNugetPackage : ICommand
    {
        public CompareNugetPackage(string packageId, string leftVersion, string rightVersion)
        {
            this.PackageId = packageId;
            RightVersion = rightVersion;
            LeftVersion = leftVersion;
        }

        public string PackageId { get; private set; }
        public string LeftVersion { get; private set; }
        public string RightVersion { get; private set; }
    }
}
