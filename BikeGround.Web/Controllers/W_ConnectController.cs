using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class W_ConnectController : Controller
    {
        // GET: Wall
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}