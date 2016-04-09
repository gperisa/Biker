using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        // GET: Friend
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}