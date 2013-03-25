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
            return View(context.PrimaryGuardianBorrows.Include(primaryguardianborrow => primaryguardianborrow.PrimaryGuardian).Include(primaryguardianborrow => primaryguardianborrow.LibraryResource).ToList());
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
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
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
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
            return View(primaryguardianborrow);
        }
        
        //
        // GET: /PrimaryGuardianBorrows/Edit/5
 
        public ActionResult Edit(int id)
        {
            PrimaryGuardianBorrow primaryguardianborrow = context.PrimaryGuardianBorrows.Single(x => x.Id == id);
            ViewBag.PossiblePrimaryGuardians = context.PrimaryGuardians;
            ViewBag.PossibleLibraryResources = context.LibraryResources;
            return View(primaryguardianborrow);
        }

        //
        // POST: /PrimaryGuardianBorrows/Edit/5

        [HttpPost]
        public ActionResult Edit(PrimaryGuardianBorrow primaryguardianborrow)
        {
            if (ModelState.IsValid)
            {
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