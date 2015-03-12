namespace APIComparer.Contracts
{
    using System;
    using NServiceBus;

    public class CompareNugetPackage : ICommand
    {
        public CompareNugetPackage(string packageId, Version leftVersion, Version rightVersion)
        {
            this.PackageId = packageId;
            RightVersion = rightVersion;
            LeftVersion = leftVersion;
        }

        public string PackageId { get; private set; }
        public Version LeftVersion { get; private set; }
        public Version RightVersion { get; private set; }
    }
}
