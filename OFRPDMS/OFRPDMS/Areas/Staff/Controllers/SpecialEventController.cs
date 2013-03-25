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
    public class SpecialEventController : Controller
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        //
        // GET: /Staff/SpecialEvent/

        public ViewResult Index()
        {
            var specialevents = db.SpecialEvents.Include(s => s.Center);
            return View(specialevents.ToList());
        }

        //
        // GET: /Staff/SpecialEvent/Details/5

        public ViewResult Details(int id)
        {
            SpecialEvent specialevent = db.SpecialEvents.Find(id);
            return View(specialevent);
        }

        //
        // GET: /Staff/SpecialEvent/Create

        public ActionResult Create()
        {
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name");
            return View();
        } 

        //
        // POST: /Staff/SpecialEvent/Create

        [HttpPost]
        public ActionResult Create(SpecialEvent specialevent)
        {
            if (ModelState.IsValid)
            {
                db.SpecialEvents.Add(specialevent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", specialevent.CenterId);
            return View(specialevent);
        }
        
        //
        // GET: /Staff/SpecialEvent/Edit/5
 
        public ActionResult Edit(int id)
        {
            SpecialEvent specialevent = db.SpecialEvents.Find(id);
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", specialevent.CenterId);
            return View(specialevent);
        }

        //
        // POST: /Staff/SpecialEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecialEvent specialevent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialevent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", specialevent.CenterId);
            return View(specialevent);
        }

        //
        // GET: /Staff/SpecialEvent/Delete/5
 
        public ActionResult Delete(int id)
        {
            SpecialEvent specialevent = db.SpecialEvents.Find(id);
            return View(specialevent);
        }

        //
        // POST: /Staff/SpecialEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SpecialEvent specialevent = db.SpecialEvents.Find(id);
            db.SpecialEvents.Remove(specialevent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}