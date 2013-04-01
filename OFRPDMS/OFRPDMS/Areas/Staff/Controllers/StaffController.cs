using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OFRPDMS.Models;
using System.Reflection;
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;


namespace OFRPDMS.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {

        private IRepositoryService repoService;
        private IAccountService account;

        public StaffController() { }

        [Inject]
        public StaffController(IAccountService account, IRepositoryService repoService) 
        {
            this.account = account;
            this.repoService = repoService;
        }

        //
        // GET: /Staff/Staff/

        public ActionResult Index(int centerIdArg)
        {
            string[] roles = account.GetRolesForUser();
            if (roles.Contains("Administrators"))
            {
                if (centerIdArg != -1)
                {
                    AccountProfile.CurrentUser.CenterID = centerIdArg;
                }

            }
            ViewBag.CurrentCenterName = db.Centers.Find(centerIdArg).Name;
            ViewBag.CurrentCenterId = "c"+ centerIdArg.ToString();

            return View();
        }



        [HttpPost]
        public ActionResult Search(string name, string type)
        {
            if (type == "Primary")
            {
                var _primaryguardian = repoService.primaryGuardianRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Country", "Email", "Language", "Phone", "PostalCodePrefix", "Allergies", "DateCreated" };
                IEnumerable<PropertyInfo> properties = typeof(PrimaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    email = pm.Email,
                    phone = pm.Phone,
                    prefix = pm.PostalCodePrefix,
                    datacreate = pm.DateCreated.ToString("MM/dd/yyyy"),
                    lang = pm.Language,
                    country = pm.Country,
                    allergy = pm.Allergies,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else if (type == "Child")
            {
                var _primaryguardian = repoService.childRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Allergies" };
                IEnumerable<PropertyInfo> properties = typeof(Child).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    relationshiptoGuardian = pm.PrimaryGuardian.FirstName,
                    allergy = pm.Allergies,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else
            {
                 var _primaryguardian = repoService.secondaryGuardianRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "RelationshipToChild" };
                IEnumerable<PropertyInfo> properties = typeof(SecondaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    relationship = pm.RelationshipToChild,
                    phone = pm.Phone,
                    relationshiptoGuardian = repoService.primaryGuardianRepo.FindById(pm.PrimaryGuardianId).FirstName

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }
    }



}
