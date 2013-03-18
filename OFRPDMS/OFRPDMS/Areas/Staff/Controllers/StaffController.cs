using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/Staff/

        public ActionResult Index(int centerIdArg)
        {
            if (centerIdArg != -1)
                AccountProfile.CurrentUser.CenterID = centerIdArg;
            return View();
        }

    }
}
