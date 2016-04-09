using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        // GET: Trip
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}