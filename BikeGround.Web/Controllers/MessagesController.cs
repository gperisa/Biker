using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}