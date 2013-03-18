using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/Staff/

        public ActionResult Index(int centerIdArg)
        {
            string[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Administrators"))
            {
                if (centerIdArg != -1)
                    AccountProfile.CurrentUser.CenterID = centerIdArg;
            }
            return View();
        }

    }
}
