﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using System.Web.Security;

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        private OFRPDMSContext context = new OFRPDMSContext();
        //
        // GET: /Admin/Admin/

        public ActionResult Index()
        {
            AccountProfile.CurrentUser.CenterID = -1;
            return View();
        }
        public ActionResult Nav()
        {
            return PartialView("_AdminNavPartial", context.Centers);
        }

        public ActionResult RegisterStaff()
        {
            ViewBag.PossibleCenters = context.Centers;
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStaff(RegisterModel model, string role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (role == "Administrators")
                    {
                        Roles.AddUserToRole(model.UserName, role);
                    }
                    else
                    {
                        Roles.AddUserToRole(model.UserName, "Staff");                        
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to add new account " + model.UserName + " to " + role);
                    return View(model);
                }
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);

                    AccountProfile.GetUser(model.UserName).CenterID = model.CenterId;
                    int id = AccountProfile.CurrentUser.CenterID;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
