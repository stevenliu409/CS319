﻿using Ninject;
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
using PagedList;
using System.Reflection;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class LibraryController : Controller
    {

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

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int centerID = account.GetCurrentUserCenterId();
            string[] roles = account.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            ViewBag.CurrentPage = "Library";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.BrokenNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.CheckedOutNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc1" : "";
            ViewBag.ValueNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc2" : "";
            ViewBag.NoteNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc3" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc4" : "";
            ViewBag.ItemTypeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc5" : "";
            ViewBag.SanitizedNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc6" : "";
            ViewBag.CheckedByNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc7" : "";
            ViewBag.CenterNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc8" : "";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var libraryitems = from p in repoService.libraryRepo.FindAllWithCenterId(centerID)
                                  select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchFields = new string[] { "ItemType","Note", "Name" };
                IEnumerable<PropertyInfo> properties = typeof(LibraryResource).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                libraryitems = libraryitems.Where(
                    p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(searchString.ToUpper()))));

            }
            switch (sortOrder)
            {
                case "Name desc":
                    libraryitems = libraryitems.OrderBy(p => p.Broken);

                    break;
                case "Name desc1":

                    libraryitems = libraryitems.OrderBy(p => p.CheckedOut);

                    break;
                case "Name desc2":

                    libraryitems = libraryitems.OrderBy(p => p.Value);

                    break;
                case "Name desc3":

                    libraryitems = libraryitems.OrderBy(p => p.Note);
                    break;
                case "Name desc4":

                    libraryitems = libraryitems.OrderBy(p => p.Name);

                    break;
                case "Name desc5":
                    libraryitems = libraryitems.OrderBy(p => p.ItemType);

                    break;

                case "Name desc6":
                    libraryitems = libraryitems.OrderBy(p => p.Sanitized);

                    break;

                case "Name desc7":
                    
                    libraryitems = libraryitems.OrderBy(p => p.Center.Name);

                    break;
                case "Name desc8":
                    libraryitems = libraryitems.OrderBy(p => p.Center.Name);

                    break;
                default:
                    libraryitems = libraryitems.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(libraryitems.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Staff/Library/Details/5

        public ActionResult Details(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            LibraryResource libraryitem = repoService.libraryRepo.FindByIdAndCenterId(id, centerID);
            PrimaryGuardianBorrow pgb = repoService.primaryGuardianBorrowsRepo.FindAllWithLibraryResourceId(id)
                .Where(p => p.Returned == false).FirstOrDefault();
            if (pgb != null)
            {
                ViewBag.borrow = pgb.Id;
            }
            return View(libraryitem);
        }

        //
        // GET: /Staff/Library/Create

        public ActionResult Create()
        {
            int centerID = account.GetCurrentUserCenterId();
            ViewBag.CenterId2 = centerID;
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name");
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
                libraryitem.CheckedOut = false;
                repoService.libraryRepo.Add(libraryitem);
                return RedirectToAction("Index");  
            }

            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindListById(centerID), "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }
        
        //
        // GET: /Staff/Library/Edit/5
 
        public ActionResult Edit(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            LibraryResource libraryitem = repoService.libraryRepo.FindByIdAndCenterId(id, centerID);
            ViewBag.CenterId2 = AccountProfile.CurrentUser.CenterID;
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
