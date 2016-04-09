using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    public class W_LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}