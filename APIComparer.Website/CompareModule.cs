namespace APIComparer.Website
{
    using APIComparer.Contracts;
    using Nancy;
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

                
                bus.Send(new CompareNugetPackage(ctx.nugetpackageid, leftVersion, rightVersion));
                return "Hello Nuget";
            };
        }
    }
}