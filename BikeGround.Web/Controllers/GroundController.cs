using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeGround.Web.Controllers
{
    public class GroundController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}