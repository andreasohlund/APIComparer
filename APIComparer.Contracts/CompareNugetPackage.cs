// ReSharper disable MemberCanBePrivate.Global
namespace APIComparer.Contracts
{
    using NServiceBus;
    using NuGet;

    public class CompareNugetPackage : ICommand
    {
        public CompareNugetPackage(string packageId, SemanticVersion leftVersion, SemanticVersion rightVersion, string target)
        {
            PackageId = packageId;
            RightVersion = rightVersion;
            LeftVersion = leftVersion;
            Target = target;
        }

        public string PackageId { get; set; }
        public SemanticVersion LeftVersion { get; set; }
        public SemanticVersion RightVersion { get; set; }
        public string Target { get; set; }
    }
}
