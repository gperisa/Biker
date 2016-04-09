using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}