namespace APIComparer.Website
{
    using System;
    using System.IO;
    using APIComparer.Contracts;
    using Nancy;
    using Nancy.Responses;
    using NServiceBus;
    using NuGet;

    public class CompareModule : NancyModule
    {
        public CompareModule(IRootPathProvider rootPathProvider, IBus bus)
        {
            Get["/compare/{nugetpackageid}/{leftversion}...{rightversion}"] = ctx =>
            {
                SemanticVersion leftVersion, rightVersion;
                if (!SemanticVersion.TryParse(ctx.leftversion, out leftVersion))
                {
                    return 404;
                }
                if (!SemanticVersion.TryParse(ctx.rightversion, out rightVersion))
                {
                    return 404;
                }

                
                var pathToAlreadyRenderedComparision = string.Format("./Comparisons/{0}-{1}...{2}.html", ctx.nugetpackageid, leftVersion, rightVersion);
              
                if (File.Exists(Path.Combine(rootPathProvider.GetRootPath(),pathToAlreadyRenderedComparision)))
                {
                    return new GenericFileResponse(pathToAlreadyRenderedComparision, "text/html");
                }

                var pathToWorkingToken = string.Format("./Comparisons/{0}-{1}...{2}.running.html", ctx.nugetpackageid, leftVersion, rightVersion);
                var fullPathToWorkingToken = Path.Combine(rootPathProvider.GetRootPath(), pathToWorkingToken);

                if (File.Exists(fullPathToWorkingToken))
                {
                    return new GenericFileResponse(pathToWorkingToken, "text/html");
                }

                bus.Send(new CompareNugetPackage(ctx.nugetpackageid, leftVersion, rightVersion));

                var fullPathToTemplate = Path.Combine(rootPathProvider.GetRootPath(), "./Comparisons/CompareRunning.html");
                File.Copy(fullPathToTemplate, fullPathToWorkingToken);
                string template = File.ReadAllText(fullPathToWorkingToken);
                string content = template.Replace(@"{packageid}", ctx.nugetpackageid.ToString())
                    .Replace(@"{leftversion}", leftVersion.ToString())
                    .Replace(@"{rightversion}", rightVersion.ToString());
                File.WriteAllText(fullPathToWorkingToken, content);

                return new GenericFileResponse(pathToWorkingToken, "text/html");
            };
        }
    }
}