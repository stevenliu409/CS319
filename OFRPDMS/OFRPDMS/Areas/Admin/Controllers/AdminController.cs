using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /Admin/Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nav()
        {

            return PartialView("_AdminNavPartial", context.Centers);
        }
    }
}
