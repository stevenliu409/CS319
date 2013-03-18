using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Controllers
{
    public class EventsController : Controller
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        //
        // GET: /Events/

        public ViewResult Index()
        {
            int centerID = AccountProfile.CurrentUser.CenterID;

            var Events = db.Events.Where(Event => Event.CenterId == centerID);
            return View(Events.ToList());
        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(int id)
        {         
            int centerID = AccountProfile.CurrentUser.CenterID;

            Event anEvent = db.Events.Where(e => e.CenterId == centerID).Single(e => e.Id == id);

            return View(anEvent);
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {          
            int centerID = AccountProfile.CurrentUser.CenterID;

            ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");

            return View();
        } 

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                anEvent.CenterId = AccountProfile.CurrentUser.CenterID;
                db.Events.Add(anEvent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            return View(anEvent);
        }
        
        //
        // GET: /Events/Edit/5
 
        public ActionResult Edit(int id)
        {

            Event anEvent;            
            int centerID = AccountProfile.CurrentUser.CenterID;

            anEvent = db.Events.Find(id);
            ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
           
            return View(anEvent);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                anEvent.CenterId = AccountProfile.CurrentUser.CenterID;
                db.Entry(anEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int centerID = AccountProfile.CurrentUser.CenterID;

            ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
           
            return View(anEvent);
        }

        //
        // GET: /Events/Delete/5
 
        public ActionResult Delete(int id)
        {
            int centerID = AccountProfile.CurrentUser.CenterID;

            Event anEvent = db.Events.Where(e => e.CenterId == centerID).Single(e => e.Id == id);

            return View(anEvent);          
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Event anEvent = db.Events.Find(id);
            db.Events.Remove(anEvent);
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
