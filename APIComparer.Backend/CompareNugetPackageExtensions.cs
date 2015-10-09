namespace APIComparer.Backend
{
    using Contracts;
    using VersionComparisons;

    public static class CompareNugetPackageExtensions
    {
        public static PackageDescription ToDescription(this CompareNugetPackage message)
        {
            return new PackageDescription
            {
                PackageId = message.PackageId,
                Versions = new VersionPair(message.LeftVersion, message.RightVersion)
            };
        }
    }
}