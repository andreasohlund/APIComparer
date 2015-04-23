namespace APIComparer.Website
{
    using System.IO;
    using APIComparer.Contracts;
    using Nancy;
    using Nancy.Responses;
    using NServiceBus;
    using NServiceBus.Logging;
    using NuGet;

    public class CompareModule : NancyModule
    {
        ILog logger = LogManager.GetLogger<CompareModule>();

        public CompareModule(IRootPathProvider rootPathProvider, IBus bus)
        {
            Get["/compare/{nugetpackageid}/{leftversion:semver}...{rightversion:semver}"] = ctx =>
            {
                var leftVersion = SemanticVersion.Parse(ctx.leftversion);
                var rightVersion = SemanticVersion.Parse(ctx.rightversion);
                var nugetPackageId = (string)ctx.nugetpackageid;

                var pathToAlreadyRenderedComparision = string.Format("./Comparisons/{0}-{1}...{2}.html", nugetPackageId, leftVersion, rightVersion);
              
                if (File.Exists(Path.Combine(rootPathProvider.GetRootPath(),pathToAlreadyRenderedComparision)))
                {
                    return new GenericFileResponse(pathToAlreadyRenderedComparision, "text/html");
                }

                var pathToWorkingToken = string.Format("./Comparisons/{0}-{1}...{2}.running.html", nugetPackageId, leftVersion, rightVersion);
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
                string template = File.ReadAllText(fullPathToWorkingToken);
                string content = template.Replace(@"{packageid}", nugetPackageId)
                    .Replace(@"{leftversion}", leftVersion.ToString())
                    .Replace(@"{rightversion}", rightVersion.ToString());

                File.WriteAllText(fullPathToWorkingToken, content);

                return new GenericFileResponse(pathToWorkingToken, "text/html");
            };
        }
    }
}