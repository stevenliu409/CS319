using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OFRPDMS.Account;
using OFRPDMS.Models;
using OFRPDMS.Repositories;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class LibraryController : Controller
    {
        private string[] ValidTypes = new string[3] { "video", "book", "toy" };

        private IRepositoryService repoService;
        private IAccountService account;

        public LibraryController() {}

        [Inject]
        public LibraryController( IAccountService account, IRepositoryService repoService)
        {
            this.repoService = repoService;
            this.account = account;
        }

        //
        // GET: /Staff/Library/

        public ActionResult Index()
        {
            int centerID = account.GetCurrentUserCenterId();

            var libraryitems = repoService.libraryRepo.FindAllWithCenterId(centerID);
            return View(libraryitems.ToList());
        }

        //
        // GET: /Staff/Library/Details/5

        public ActionResult Details(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            LibraryResource libraryitem = repoService.libraryRepo.FindByIdAndCenterId(id, centerID);
            return View(libraryitem);
        }

        //
        // GET: /Staff/Library/Create

        public ActionResult Create()
        {
            int centerID = account.GetCurrentUserCenterId();

            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name");
            ViewBag.ItemTypes = new SelectList(ValidTypes.AsEnumerable());
            return View();
        } 

        //
        // POST: /Staff/Library/Create

        [HttpPost]
        public ActionResult Create(LibraryResource libraryitem)
        {
            int centerID = account.GetCurrentUserCenterId();

            if (ModelState.IsValid)
            {
                repoService.libraryRepo.Add(libraryitem);
                return RedirectToAction("Index");  
            }

            ViewBag.ItemType = new SelectList(ValidTypes.AsEnumerable());
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }
        
        //
        // GET: /Staff/Library/Edit/5
 
        public ActionResult Edit(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            LibraryResource libraryitem = repoService.libraryRepo.FindByIdAndCenterId(id, centerID);
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }

        //
        // POST: /Staff/Library/Edit/5

        [HttpPost]
        public ActionResult Edit(LibraryResource libraryitem)
        {
            int centerID = account.GetCurrentUserCenterId();

            if (ModelState.IsValid)
            {
                repoService.libraryRepo.Update(libraryitem);
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }

        //
        // GET: /Staff/Library/Delete/5
 
        public ActionResult Delete(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            LibraryResource libraryitem = repoService.libraryRepo.FindByIdAndCenterId(id, centerID);
            return View(libraryitem);
        }

        //
        // POST: /Staff/Library/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryResource libraryitem = repoService.libraryRepo.FindById(id);
            repoService.libraryRepo.Delete(libraryitem);
            return RedirectToAction("Index");
        }


        // verifies the given center id is valid for current user
        // then sets CenterId in the viewbag if valid
        private bool VerifyCenterId(int id)
        {

            // Administrator
            if (User.IsInRole("Administrators"))
            {
                ViewBag.StaffCenterId = id;
                return true; // always valid for administrator
            }
            // Staff
            else
            {
                
                int staffCenterId = account.GetCurrentUserCenterId();
                if (staffCenterId == id)
                {
                    ViewBag.StaffCenterId = id;
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Insufficient privileges to access the center. Please request an account from the administrator.");
                    return false;
                }
            }
        }



        protected override void Dispose(bool disposing)
        {
            repoService.libraryRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
