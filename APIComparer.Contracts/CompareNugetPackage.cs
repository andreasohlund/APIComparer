// ReSharper disable MemberCanBePrivate.Global
namespace APIComparer.Contracts
{
    using NServiceBus;
    using NuGet;

    public class CompareNugetPackage : ICommand
    {
        public CompareNugetPackage(string packageId, SemanticVersion leftVersion, SemanticVersion rightVersion)
        {
            PackageId = packageId;
            RightVersion = rightVersion;
            LeftVersion = leftVersion;
        }

        public string PackageId { get; set; }
        public SemanticVersion LeftVersion { get; set; }
        public SemanticVersion RightVersion { get; set; }
    }
}
