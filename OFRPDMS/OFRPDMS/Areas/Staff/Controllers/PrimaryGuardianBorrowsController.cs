using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class PrimaryGuardianBorrowsController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /PrimaryGuardianBorrows/

        public ViewResult Index()
        {
            int centerid = AccountProfile.CurrentUser.CenterID;
            return View(context.PrimaryGuardianBorrows.Where(pgb => pgb.LibraryResource.CenterId == centerid).Include(primaryguardianborrow => primaryguardianborrow.PrimaryGuardian).Include(primaryguardianborrow => primaryguardianborrow.LibraryResource).ToList());
        }

        //
        // GET: /PrimaryGuardianBorrows/Details/5

        public ViewResult Details(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = context.PrimaryGuardianBorrows.Single(x => x.Id == id);
            return View(primaryguardianborrow);
        }

        //
        // GET: /PrimaryGuardianBorrows/Create

        public ActionResult Create()
        {
            int centerid = AccountProfile.CurrentUser.CenterID;
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources.Where(lr => lr.CheckedOut == false && lr.CenterId == centerid);

            return View();
        } 

        //
        // POST: /PrimaryGuardianBorrows/Create

        [HttpPost]
        public ActionResult Create(PrimaryGuardianBorrow primaryguardianborrow)
        {
            if (ModelState.IsValid)
            {
                
                context.PrimaryGuardianBorrows.Add(primaryguardianborrow);
                primaryguardianborrow.BorrowDate = System.DateTime.Now;
                context.LibraryResources.Find(primaryguardianborrow.LibraryResourceId).CheckedOut = true;
                primaryguardianborrow.DueDate = primaryguardianborrow.DueDate.AddHours(23);
                primaryguardianborrow.DueDate = primaryguardianborrow.DueDate.AddMinutes(59);

                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources.Where(lr=> lr.CheckedOut == false);
            return View(primaryguardianborrow);
        }
        
        //
        // We took out the edit function and turned it into check in function. The edit view is no longer in use
        public ActionResult Edit(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = context.PrimaryGuardianBorrows.Single(x => x.Id == id);
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
            if (primaryguardianborrow.Returned != true)
            {
                primaryguardianborrow.Returned = true;
                primaryguardianborrow.ReturnDate = System.DateTime.Now;
                LibraryResource res = context.LibraryResources.Find(primaryguardianborrow.LibraryResourceId);
                res.CheckedOut = false;
                context.Entry(res).State = EntityState.Modified;
                context.Entry(primaryguardianborrow).State = EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //
        // POST: we took out the edit function and changed it into a checked in function. The edit view is no longer in use

        [HttpPost]
        public ActionResult Edit(PrimaryGuardianBorrow primaryguardianborrow)
        {
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
                context.Entry(primaryguardianborrow).State = EntityState.Modified;
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
            PrimaryGuardianBorrow primaryguardianborrow = context.PrimaryGuardianBorrows.Single(x => x.Id == id);
            return View(primaryguardianborrow);
        }

        //
        // POST: /PrimaryGuardianBorrows/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = context.PrimaryGuardianBorrows.Single(x => x.Id == id);
            context.PrimaryGuardianBorrows.Remove(primaryguardianborrow);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}