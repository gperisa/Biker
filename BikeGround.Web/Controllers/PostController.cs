using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            return PartialView();
        }
    }
}