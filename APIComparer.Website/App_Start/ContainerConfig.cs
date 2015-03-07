namespace APIComparer.Website
{
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;

    public static class ContainerConfig
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}