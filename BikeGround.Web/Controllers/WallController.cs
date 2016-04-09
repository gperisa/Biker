using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class WallController : Controller
    {
        // GET: Wall
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}