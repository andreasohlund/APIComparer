namespace APIComparer.Website
{
    using System.Collections.Generic;
    using Autofac;
    using Nancy.Bootstrappers.Autofac;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new NuGetBrowser(new List<string>
            {
                "https://www.nuget.org/api/v2"
            }));

            BusConfig.Setup(existingContainer);
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}