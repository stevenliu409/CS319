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
    public class PrimaryGuardiansController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /PrimaryGuardians/

        public ViewResult Index()
        {
            return View(context.PrimaryGuardians.Include(primaryguardian => primaryguardian.PrimaryGuardianBorrows).Include(primaryguardian => primaryguardian.EventParticipants).Include(primaryguardian => primaryguardian.Children).ToList());
        }

        //
        // GET: /PrimaryGuardians/Details/5

        public ViewResult Details(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Single(x => x.Id == id);
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PrimaryGuardians/Create

        [HttpPost]
        public ActionResult Create(PrimaryGuardian primaryguardian)
        {
            if (ModelState.IsValid)
            {
                context.PrimaryGuardians.Add(primaryguardian);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(primaryguardian);
        }
        
        //
        // GET: /PrimaryGuardians/Edit/5
 
        public ActionResult Edit(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Single(x => x.Id == id);
            return View(primaryguardian);
        }

        //
        // POST: /PrimaryGuardians/Edit/5

        [HttpPost]
        public ActionResult Edit(PrimaryGuardian primaryguardian)
        {
            if (ModelState.IsValid)
            {
                context.Entry(primaryguardian).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Delete/5
 
        public ActionResult Delete(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Single(x => x.Id == id);
            return View(primaryguardian);
        }

        //
        // POST: /PrimaryGuardians/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Single(x => x.Id == id);
            context.PrimaryGuardians.Remove(primaryguardian);
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