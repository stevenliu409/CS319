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
        private OFRPDMSContext db = new OFRPDMSContext();

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
            ViewBag.CurrentCenterId = "c"+ centerIdArg.ToString();
            //ViewBag.CurrentCenterName = db.Centers.Find(centerIdArg).Name;
            return View();
        }




        [HttpPost]
        public ActionResult Search(string name, string type)
        {
            if (type == "Primary")
            {
                var _primaryguardian = db.PrimaryGuardians.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name) || 
                    p.Allergies.Contains(name) || p.Country.Contains(name) || p.Language.Contains(name) || p.Email.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    email = pm.Email,
                    phone = pm.Phone,
                    prefix = pm.PostalCodePrefix,
                    datacreate = pm.DateCreated.ToString(),
                    lang = pm.Language,
                    country = pm.Country,
                    allergy = pm.Allergies

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else if (type == "Child")
            {
                var _primaryguardian = db.Children.Where(c => c.FirstName.Contains(name) || c.Allergies.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    allergy = pm.Allergies,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var _primaryguardian = db.SecondaryGuardians.Where(s => s.FirstName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }
    }



}
