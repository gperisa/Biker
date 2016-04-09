using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}