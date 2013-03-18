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
            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                var Events = db.Events.Include(e => e.Center);
                return View(Events.ToList());
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                var Events = db.Events.Where(Event => Event.CenterId == centerID);
                return View(Events.ToList());
            }
        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(int id)
        {
            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                Event anEvent = db.Events.Find(id);
                return View(anEvent);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                Event anEvent = db.Events.Where(e => e.CenterId == centerID).Single(e => e.Id == id);

                return View(anEvent);
            }
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name");
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
            }

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
                anEvent.Center = db.Centers.Find(anEvent.CenterId);
                db.Events.Add(anEvent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            string[] roles = Roles.GetRolesForUser();
            if (roles.Any(item => item == "Administrators"))
            {
                ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name");
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
            }

            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", anEvent.CenterId);
            return View(anEvent);
        }
        
        //
        // GET: /Events/Edit/5
 
        public ActionResult Edit(int id)
        {
            string[] roles = Roles.GetRolesForUser();

            Event anEvent;
            if (roles.Any(item => item == "Administrators"))
            {
                anEvent = db.Events.Find(id);
                ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", anEvent.CenterId);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                anEvent = db.Events.Find(id);
                ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
            }
            return View(anEvent);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", anEvent.CenterId);
            }
            else
            {

                int centerID = AccountProfile.CurrentUser.CenterID;

                ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerID), "Id", "Name");
            }
            return View(anEvent);
        }

        //
        // GET: /Events/Delete/5
 
        public ActionResult Delete(int id)
        {
            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                Event anEvent = db.Events.Find(id);
                return View(anEvent);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                Event anEvent = db.Events.Where(e => e.CenterId == centerID).Single(e => e.Id == id);

                return View(anEvent);
            }
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
