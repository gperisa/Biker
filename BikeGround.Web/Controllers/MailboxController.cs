using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    [Authorize]
    public class MailboxController : Controller
    {
        // GET: Mailbox
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}