namespace APIComparer.Website
{
    using System;
    using APIComparer.Contracts;
    using Nancy;
    using NServiceBus;

    public class CompareModule : NancyModule
    {
        public CompareModule(IBus bus)
        {
            Get["/compare/{nugetpackageid}/{leftversion}...{rightversion}"] = ctx =>
            {
                Version leftVersion, rightVersion;
                if (!Version.TryParse(ctx.leftversion, out leftVersion))
                {
                    return 404;
                }
                if (!Version.TryParse(ctx.rightversion, out rightVersion))
                {
                    return 404;
                }

                bus.Send(new CompareNugetPackage(ctx.nugetpackageid, leftVersion, rightVersion));
                return "Hello Nuget";
            };
        }
    }
}