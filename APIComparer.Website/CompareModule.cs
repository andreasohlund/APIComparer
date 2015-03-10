namespace APIComparer.Website
{
    using System.Threading.Tasks;
    using Nancy;

    public class CompareModule : NancyModule
    {
        public CompareModule()
        {
            Get["/compare/{nugetpackageid}/{leftversion:version}...{rightversion:version}", true] = async (ctx, token) =>
            {
                return Task.FromResult(200);
            };
        }
    }
}