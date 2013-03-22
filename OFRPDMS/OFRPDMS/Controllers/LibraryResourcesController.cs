using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Controllers
{   
    public class LibraryResourcesController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /LibraryResources/

        public ViewResult Index()
        {
            return View(context.LibraryResources.Include(libraryresource => libraryresource.Center).Include(libraryresource => libraryresource.PrimaryGuardianBorrows).ToList());
        }

        //
        // GET: /LibraryResources/Details/5

        public ViewResult Details(int id)
        {
            LibraryResource libraryresource = context.LibraryResources.Single(x => x.Id == id);
            return View(libraryresource);
        }

        //
        // GET: /LibraryResources/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = context.Centers;
            return View();
        } 

        //
        // POST: /LibraryResources/Create

        [HttpPost]
        public ActionResult Create(LibraryResource libraryresource)
        {
            if (ModelState.IsValid)
            {
                context.LibraryResources.Add(libraryresource);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCenters = context.Centers;
            return View(libraryresource);
        }
        
        //
        // GET: /LibraryResources/Edit/5
 
        public ActionResult Edit(int id)
        {
            LibraryResource libraryresource = context.LibraryResources.Single(x => x.Id == id);
            ViewBag.PossibleCenters = context.Centers;
            return View(libraryresource);
        }

        //
        // POST: /LibraryResources/Edit/5

        [HttpPost]
        public ActionResult Edit(LibraryResource libraryresource)
        {
            if (ModelState.IsValid)
            {
                context.Entry(libraryresource).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleCenters = context.Centers;
            return View(libraryresource);
        }

        //
        // GET: /LibraryResources/Delete/5
 
        public ActionResult Delete(int id)
        {
            LibraryResource libraryresource = context.LibraryResources.Single(x => x.Id == id);
            return View(libraryresource);
        }

        //
        // POST: /LibraryResources/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryResource libraryresource = context.LibraryResources.Single(x => x.Id == id);
            context.LibraryResources.Remove(libraryresource);
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