using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}