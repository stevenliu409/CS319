using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using PagedList;
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class PrimaryGuardianBorrowsController : Controller
    {
        
        private IRepositoryService repoService;
        private IAccountService account;

        public PrimaryGuardianBorrowsController() { }

        [Inject]
        public PrimaryGuardianBorrowsController(IAccountService account, IRepositoryService repoService)
        {
            this.account = account;
            this.repoService = repoService;

        }

        //
        // GET: /PrimaryGuardianBorrows/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page )
        {

            int centerid = account.GetCurrentUserCenterId();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.PrimaryNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.ResourceNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc1" : "";
            ViewBag.ReturnNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc2" : "";

            ViewBag.BorrowDateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            ViewBag.DueDateSortParm = sortOrder == "Date1" ? "Date desc1" : "Date";
            ViewBag.ReturnDateSortParm = sortOrder == "Date2" ? "Date desc2" : "Date";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
             OFRPDMSContext context = new OFRPDMSContext();
            var pgbs = from p in context.PrimaryGuardianBorrows.Where(pgb => pgb.LibraryResource.CenterId == centerid).Include(primaryguardianborrow => primaryguardianborrow.PrimaryGuardian).Include(primaryguardianborrow => primaryguardianborrow.LibraryResource)
                               select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pgbs = pgbs.Where(p => p.PrimaryGuardian.FirstName.ToUpper().Contains(searchString.ToUpper()) || p.LibraryResource.Name.ToUpper().Contains(searchString.ToUpper()));
                           

            }
            switch (sortOrder)
            {
                case "Name desc":
                    pgbs = pgbs.OrderByDescending(p => p.PrimaryGuardian.FirstName);

                    break;
                case "Name desc1":

                    pgbs = pgbs.OrderByDescending(p => p.LibraryResource.Name);

                    break;
                case "Name desc2":

                    pgbs = pgbs.OrderByDescending(p => p.Returned);

                    break;

                case "Date":
                    pgbs = pgbs.OrderBy(s => s.BorrowDate);
                    break;
                case "Date desc":
                    pgbs = pgbs.OrderByDescending(s => s.BorrowDate);
                    break;
                case "Date1":
                    pgbs = pgbs.OrderBy(s => s.DueDate);
                    break;
                case "Date desc1":
                    pgbs = pgbs.OrderByDescending(s => s.DueDate);
                    break;
                case "Date2":
                    pgbs = pgbs.OrderBy(s => s.ReturnDate);
                    break;
                case "Date desc2":
                    pgbs = pgbs.OrderByDescending(s => s.DueDate);
                    break;

                default:
                    pgbs = pgbs.OrderBy(s => s.BorrowDate);
                    break;
            }



            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(pgbs.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PrimaryGuardianBorrows/Details/5

        public ViewResult Details(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = repoService.primaryGuardianBorrowsRepo.FindById(id);
            return View(primaryguardianborrow);
        }

        //
        // GET: /PrimaryGuardianBorrows/Create

        public ActionResult Create()
        {
            OFRPDMSContext context = new OFRPDMSContext();
            int centerid = account.GetCurrentUserCenterId();
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources.Where(lr => lr.CheckedOut == false && lr.CenterId == centerid);

            return View();
        } 

        //
        // POST: /PrimaryGuardianBorrows/Create


        [HttpPost]
        public ActionResult Create(PrimaryGuardianBorrow primaryguardianborrow)
        {
            OFRPDMSContext context = new OFRPDMSContext();
            if (ModelState.IsValid)
            {
                primaryguardianborrow.BorrowDate = System.DateTime.Now;
                context.LibraryResources.Find(primaryguardianborrow.LibraryResourceId).CheckedOut = true;
                primaryguardianborrow.DueDate = primaryguardianborrow.DueDate.AddHours(23);
                primaryguardianborrow.DueDate = primaryguardianborrow.DueDate.AddMinutes(59);
                repoService.primaryGuardianBorrowsRepo.Insert(primaryguardianborrow);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources.Where(lr => lr.CheckedOut == false);
            return View(primaryguardianborrow);
        }
        
        //
        // We took out the edit function and turned it into check in function. The edit view is no longer in use
        public ActionResult Edit(int id)
        {
            OFRPDMSContext context = new OFRPDMSContext();
            PrimaryGuardianBorrow primaryguardianborrow = repoService.primaryGuardianBorrowsRepo.FindById(id);
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
            if (primaryguardianborrow.Returned != true)
            {
                primaryguardianborrow.Returned = true;
                primaryguardianborrow.ReturnDate = System.DateTime.Now;
                LibraryResource res = context.LibraryResources.Find(primaryguardianborrow.LibraryResourceId);
                res.CheckedOut = false;
                context.Entry(res).State = EntityState.Modified;
                repoService.primaryGuardianBorrowsRepo.Update(primaryguardianborrow);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //
        // POST: we took out the edit function and changed it into a checked in function. The edit view is no longer in use

        [HttpPost]
        public ActionResult Edit(PrimaryGuardianBorrow primaryguardianborrow)
        {
            OFRPDMSContext context = new OFRPDMSContext();
            if (ModelState.IsValid)
            {
                if (primaryguardianborrow.Returned)
                {
                    context.LibraryResources.Find(primaryguardianborrow.LibraryResourceId).CheckedOut = false;
                }
                else
                {
                    return View(primaryguardianborrow);
                }
                primaryguardianborrow.ReturnDate = System.DateTime.Now;
                repoService.primaryGuardianBorrowsRepo.Update(primaryguardianborrow);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
            return View(primaryguardianborrow);
        }

        //
        // GET: /PrimaryGuardianBorrows/Delete/5
 
        public ActionResult Delete(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = repoService.primaryGuardianBorrowsRepo.FindById(id);
            return View(primaryguardianborrow);
        }

        //
        // POST: /PrimaryGuardianBorrows/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = repoService.primaryGuardianBorrowsRepo.FindById(id);
            repoService.primaryGuardianBorrowsRepo.Delete(primaryguardianborrow);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repoService.primaryGuardianBorrowsRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}