namespace APIComparer.Website
{
    using System.Reflection;
    using Nancy;

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = parameters =>
              {
                  var fileVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                  return "Hello World - v" + fileVersion;
              };
        }
    }
}