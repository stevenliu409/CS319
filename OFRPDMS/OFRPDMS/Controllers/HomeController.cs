using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using OFRPDMS.Models;

namespace OFRPDMS.Controllers
{
    public class HomeController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();
        private PrimaryGuardian _primaryguardian;

        public HomeController()
        {
      
            _primaryguardian =context.PrimaryGuardians.FirstOrDefault();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GetParticipants(String query)
        {
            if (_primaryguardian == null)
                return View();
            else
            {
                var participants = _primaryguardian.getFirstName(query);
                return Json(participants, JsonRequestBehavior.AllowGet);
            }
   
        }
    }
}
