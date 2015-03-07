namespace APIComparer.Website
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using NServiceBus;

    public class MvcApplication : HttpApplication
    {
        IBus bus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = ContainerConfig.Setup();
            bus = BusConfig.Setup(container);
        }

        protected void Application_End()
        {
            bus.Dispose();
        }
    }
}
