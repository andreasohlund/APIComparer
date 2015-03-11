using System.Collections.Generic;
using System.Linq;
using NuGet;

class NuGetBrowser
{
    IPackageRepository repository;

 
    public NuGetBrowser(IEnumerable<string> repositories)
    {
        var reposToUse = new List<IPackageRepository>();

        reposToUse.AddRange(repositories.ToList().Select(r => PackageRepositoryFactory.Default.CreateRepository(r)));
        repository = new AggregateRepository(reposToUse);
    }

    public IList<SemanticVersion> GetAllVersions(string package,bool includePreReleases = false)
    {

        var packages = repository.FindPackagesById(package).ToList();

        return packages.Where(item => (item.IsReleaseVersion()||includePreReleases) && item.IsListed())
            .Select(p => p.Version)
            .ToList();

    }
}