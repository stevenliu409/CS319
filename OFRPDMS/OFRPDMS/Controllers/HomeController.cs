using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Controllers
{
    public class HomeController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Nav()
        {

            return PartialView("_AdminNavPartial", context.Centers);
        }
    }
}
