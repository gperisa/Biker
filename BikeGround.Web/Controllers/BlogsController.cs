using System;
using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Post
        public ActionResult Index(string ID)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            if (String.IsNullOrEmpty(ID))
            {
                return HttpNotFound();
            }

            ViewBag.ID = ID;

            return PartialView();
        }
    }
}