using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using System.Web.Security;
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class SpecialEventController : Controller
    {

        private IRepositoryService repoService;
        private IAccountService account;
       
        //
        // GET: /Staff/SpecialEvent/

        public SpecialEventController() {}

        [Inject]
        public SpecialEventController(IAccountService account, IRepositoryService repoService)
        {
            this.repoService = repoService;
            this.account = account;
        }


        public ViewResult Index()
        {
            int centerID = account.GetCurrentUserCenterId();
            string[] roles = account.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            var specialevents = repoService.specialEventRepo.FindAllWithCenterId(centerID);
            return View(specialevents.ToList());
        }

        //
        // GET: /Staff/SpecialEvent/Details/5

        public ViewResult Details(int id)
        {
             int centerID = account.GetCurrentUserCenterId();
            SpecialEvent specialevent = repoService.specialEventRepo.FindByIdAndCenterId(id,centerID);
            return View(specialevent);
        }

        //
        // GET: /Staff/SpecialEvent/Create

        public ActionResult Create()
        {
            ViewBag.CenterId = account.GetCurrentUserCenterId();
            return View();
        } 

        //
        // POST: /Staff/SpecialEvent/Create

        [HttpPost]
        public ActionResult Create(SpecialEvent specialevent)
        {
            if (ModelState.IsValid)
            {
                specialevent.CenterId = account.GetCurrentUserCenterId();
                repoService.specialEventRepo.Insert(specialevent);
                return RedirectToAction("Index");  
            }
            ViewBag.CenterId = account.GetCurrentUserCenterId();
            return View(specialevent);
        }
        
        //
        // GET: /Staff/SpecialEvent/Edit/5
 
        public ActionResult Edit(int id)
        {
            SpecialEvent specialevent = repoService.specialEventRepo.FindById(id);
            ViewBag.CenterId = account.GetCurrentUserCenterId();
            return View(specialevent);
        }

        //
        // POST: /Staff/SpecialEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecialEvent specialevent)
        {
            if (ModelState.IsValid)
            {
                specialevent.CenterId = account.GetCurrentUserCenterId();
                repoService.specialEventRepo.Update(specialevent);
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = account.GetCurrentUserCenterId();
            return View(specialevent);
        }

        //
        // GET: /Staff/SpecialEvent/Delete/5
 
        public ActionResult Delete(int id)
        {
            SpecialEvent specialevent = repoService.specialEventRepo.FindById(id);
            return View(specialevent);
        }

        //
        // POST: /Staff/SpecialEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecialEvent specialevent = repoService.specialEventRepo.FindById(id);
            repoService.specialEventRepo.Delete(specialevent);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            repoService.specialEventRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}