namespace APIComparer.Website
{
    using System.Collections.Generic;
    using System.Linq;
    using NuGet;

    public class NuGetBrowser
    {
        public NuGetBrowser(IEnumerable<string> repositories)
        {
            var reposToUse = new List<IPackageRepository>();

            reposToUse.AddRange(repositories.ToList().Select(r => PackageRepositoryFactory.Default.CreateRepository(r)));
            repository = new AggregateRepository(reposToUse);
        }

        public IList<SemanticVersion> GetAllVersions(string package)
        {
            var packages = repository.FindPackagesById(package).ToList();

            return packages.Where(item => item.IsReleaseVersion() && item.IsListed())
                .Select(p => p.Version)
                .ToList();
        }

        private IPackageRepository repository;
    }
}