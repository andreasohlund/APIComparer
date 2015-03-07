namespace APIComparer.Website
{
    using System.Web.Mvc;
    using System.Web.Mvc.Routing;
    using System.Web.Routing;
    using APIComparer.Website.Controllers;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("semver", typeof(SemverConstraint));
            routes.MapMvcAttributeRoutes(constraintsResolver);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Compare", id = UrlParameter.Optional }
                );
        }
    }
}
