namespace APIComparer.Website
{
    using System;
    using Nancy.Routing.Constraints;

    public class VersionRouteSegmentConstraint : RouteSegmentConstraintBase<Version>
    {
        public override string Name
        {
            get { return "version"; }
        }

        protected override bool TryMatch(string constraint, string segment, out Version matchedValue)
        {
            return Version.TryParse(segment, out matchedValue);
        }
    }
}