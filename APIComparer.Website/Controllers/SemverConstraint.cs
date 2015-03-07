namespace APIComparer.Website.Controllers
{
    using System.Web;
    using System.Web.Routing;
    using NuGet;

    public class SemverConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            SemanticVersion version;
            object value;
            return values.TryGetValue(parameterName, out value) && value != null && SemanticVersion.TryParse(value.ToString(), out version) && version != null;
        }
    }
}