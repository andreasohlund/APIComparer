namespace APIComparer.Website
{
    using System.IO;
    using System.Linq;
    using Contracts;
    using Nancy;
    using Nancy.Responses;
    using NServiceBus;
    using NServiceBus.Logging;
    using NuGet;

    public class CompareModule : NancyModule
    {
        public CompareModule(IRootPathProvider rootPathProvider, IBus bus, NuGetBrowser nuGetBrowser)
        {
            Get["/compare/{nugetpackageid}/{leftversion}...{rightversion}"] = ctx =>
            {
                var nugetPackageId = (string) ctx.nugetpackageid;

                SemanticVersion leftVersion;
                SemanticVersion rightVersion;

                var redirectToExactComparison = false;

                try
                {
                    if (TryExpandVersion(nuGetBrowser, nugetPackageId, ctx.leftversion, out leftVersion))
                    {
                        redirectToExactComparison = true;
                    }
                    if (TryExpandVersion(nuGetBrowser, nugetPackageId, ctx.rightversion, out rightVersion))
                    {
                        redirectToExactComparison = true;
                    }
                }
                catch (NotFoundException ex) //for now
                {
                    return new NotFoundResponse
                    {
                        ReasonPhrase = ex.Message
                    };
                }

                if (redirectToExactComparison)
                {
                    return Response.AsRedirect($"/compare/{nugetPackageId}/{leftVersion}...{rightVersion}");
                }

                var pathToAlreadyRenderedComparison = $"./Comparisons/{nugetPackageId}-{leftVersion}...{rightVersion}.html";

                if (File.Exists(Path.Combine(rootPathProvider.GetRootPath(), pathToAlreadyRenderedComparison)))
                {
                    return new GenericFileResponse(pathToAlreadyRenderedComparison, "text/html");
                }

                var pathToWorkingToken = $"./Comparisons/{nugetPackageId}-{leftVersion}...{rightVersion}.running.html";
                var fullPathToWorkingToken = Path.Combine(rootPathProvider.GetRootPath(), pathToWorkingToken);

                if (File.Exists(fullPathToWorkingToken))
                {
                    return new GenericFileResponse(pathToWorkingToken, "text/html");
                }

                logger.DebugFormat("Sending command to backend to process '{0}' package versions '{1}' and '{2}'.",
                    nugetPackageId, leftVersion, rightVersion);
                bus.Send(new CompareNugetPackage(nugetPackageId, leftVersion.ToString(), rightVersion.ToString()));

                var fullPathToTemplate = Path.Combine(rootPathProvider.GetRootPath(), "./Comparisons/CompareRunning.html");
                File.Copy(fullPathToTemplate, fullPathToWorkingToken);
                var template = File.ReadAllText(fullPathToWorkingToken);
                var content = template.Replace(@"{packageid}", nugetPackageId)
                    .Replace(@"{leftversion}", leftVersion.ToString())
                    .Replace(@"{rightversion}", rightVersion.ToString());

                File.WriteAllText(fullPathToWorkingToken, content);

                return new GenericFileResponse(pathToWorkingToken, "text/html");
            };
        }

        bool TryExpandVersion(NuGetBrowser nuGetBrowser, string nugetPackageId, string requestedVersion, out SemanticVersion expandedVersion)
        {
            var parts = requestedVersion.Split('.');

            if (parts.Length > 2 || !parts.Any())
            {
                expandedVersion = SemanticVersion.Parse(requestedVersion);
                return false;
            }

            if (parts.Length == 2)
            {
                expandedVersion = nuGetBrowser.GetAllVersions(nugetPackageId)
                    .Where(p => p.Version.Major == int.Parse(parts[0]) && p.Version.Minor == int.Parse(parts[1]))
                    .OrderByDescending(p => p.Version)
                    .FirstOrDefault();
            }
            else
            {
                expandedVersion = nuGetBrowser.GetAllVersions(nugetPackageId)
                    .Where(p => p.Version.Major == int.Parse(parts[0]))
                    .OrderByDescending(p => p.Version)
                    .FirstOrDefault();
            }

            if (expandedVersion == null)
            {
                throw new NotFoundException($"Can't find any versions for {nugetPackageId} matching version pattern {requestedVersion}.*");
            }

            return true;
        }

        ILog logger = LogManager.GetLogger<CompareModule>();
    }
}