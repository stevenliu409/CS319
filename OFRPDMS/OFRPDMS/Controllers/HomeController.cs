using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OFRPDMS.Models;
using OFRPDMS.Account;
using OFRPDMS.Repositories;

using System.Data;
using System.Data.Entity;

namespace OFRPDMS.Controllers
{
    public class HomeController : Controller
    {
        private IAccountService accountService;
        private IRepositoryService repoService;
        private OFRPDMSContext db = new OFRPDMSContext();
        public HomeController() {}

        [Inject]
        public HomeController(IAccountService accountService, IRepositoryService repoService)
        {
            this.accountService = accountService;
            this.repoService = repoService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            if (!accountService.RoleExists("Administrators"))
            {
                accountService.CreateRole("Administrators");
            }
            if (!accountService.RoleExists("Staff"))
            {
                accountService.CreateRole("Staff");

            }
            if (accountService.GetRolesForUser().Contains("Administrators"))
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            if (accountService.GetRolesForUser().Contains("Staff"))
                return RedirectToRoute("Staff_default", new { centerIdArg = -1, controller = "Staff", action = "Index" });

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult About()
        {
            return View();
        }
        /*
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
         * */

        public ActionResult Nav()
        {
            return PartialView("_AdminNavPartial", repoService.centerRepo.FindAll());
        }

        public ActionResult CurrentCenter()
        {
            Center c = db.Centers.Find(AccountProfile.CurrentUser.CenterID);
            if (AccountProfile.CurrentUser.CenterID == -1)
            {
                c = new Center();
                c.Name = "Admin";
            }
            return PartialView("_CurrentCenterPartial", c);
        }
    }
}
