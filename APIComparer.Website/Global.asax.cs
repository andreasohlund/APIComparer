namespace APIComparer.Website
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Integration.Mvc;
    using NServiceBus;
    using NServiceBus.Features;

    public class MvcApplication : HttpApplication
    {
        IStartableBus startableBus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var configuration = new BusConfiguration();
            configuration.AssembliesToScan(AllAssemblies.Except("Windows.Azure.*"));
            WireupAutofacContainer(configuration);

            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();

            startableBus = Bus.Create(configuration);
            startableBus.Start();
        }

        private static void WireupAutofacContainer(BusConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            configuration.UseContainer<AutofacBuilder>(x => x.ExistingLifetimeScope(container));
        }

        protected void Application_End()
        {
            startableBus.Dispose();
        }
    }
}
