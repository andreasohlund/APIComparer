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
        public CompareModule(IBus bus)
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

                var rootPath = Environment.GetEnvironmentVariable("HOME") ?? @".\";

                var pathToAlreadyRenderedComparision = Path.Combine(rootPath, "Comparisons", string.Format("{0}-{1}..{2}.html", ctx.nugetpackageid, leftVersion, rightVersion));

                if (File.Exists(pathToAlreadyRenderedComparision))
                {
                    return new GenericFileResponse(pathToAlreadyRenderedComparision,"text/html");
                }
                
                bus.Send(new CompareNugetPackage(ctx.nugetpackageid, leftVersion, rightVersion));
                return "TODO refresh";
            };
        }
    }
}