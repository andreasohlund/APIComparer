namespace APIComparer.Website
{
    using Autofac;
    using Nancy.Bootstrappers.Autofac;

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();

            BusConfig.Setup(existingContainer);
            builder.Update(existingContainer.ComponentRegistry);
        }
    }
}