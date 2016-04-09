using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}