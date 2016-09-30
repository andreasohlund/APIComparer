// ReSharper disable MemberCanBePrivate.Global
namespace APIComparer.Contracts
{
    using NServiceBus;

    public class CompareNugetPackage : ICommand
    {
        public CompareNugetPackage(string packageId, string leftVersion, string rightVersion)
        {
            PackageId = packageId;
            RightVersion = rightVersion.ToLower();
            LeftVersion = leftVersion.ToLower();
        }

        public string PackageId { get; set; }
        public string LeftVersion { get; set; }
        public string RightVersion { get; set; }
    }
}
