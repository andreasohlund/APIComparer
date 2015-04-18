namespace APIComparer.Website
{
    using Nancy.Routing.Constraints;
    using NuGet;

    public class SemanticVersionRouteConstraint : RouteSegmentConstraintBase<SemanticVersion>
    {
        public override string Name
        {
            get { return "semver"; }
        }

        protected override bool TryMatch(string constraint, string segment, out SemanticVersion matchedValue)
        {
            return SemanticVersion.TryParse(segment, out matchedValue);
        }
    }
}