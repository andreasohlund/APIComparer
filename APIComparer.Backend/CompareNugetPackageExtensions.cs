namespace APIComparer.Backend
{
    using APIComparer.Contracts;
    using APIComparer.VersionComparisons;

    public static class CompareNugetPackageExtensions
    {
        public static PackageDescription ToDescription(this CompareNugetPackage message)
        {
            return new PackageDescription { PackageId = message.PackageId, Versions = new VersionPair(message.LeftVersion, message.RightVersion) };
        }
    }
}