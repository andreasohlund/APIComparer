namespace APIComparer.Website.Controllers
{
    using System.Web.Mvc;
    using NServiceBus;

    [RoutePrefix("Compare")]
    public class CompareController : Controller
    {
        public IBus Bus { get; set; }

        [Route("{nugetpackageid}/{leftVersion}...{rightVersion}")]
        public ActionResult CompareVersions(string nugetPackageId, string leftVersion, string rightVersion)
        {
            return View();
        }

        [Route("{nugetpackageid}")]
        public ActionResult CompareLatestStable(string nugetPackageId)
        {
            return View();
        }
    }
}