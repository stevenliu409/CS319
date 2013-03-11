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
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /Events/

        public ViewResult Index()
        {
            string[] roles = Roles.GetRolesForUser();

            if (roles.Any(item => item == "Administrators"))
            {
                //return View(context.Events.Include(Event => Event.Center).Include(Event => Event.EventParticipants).ToList());
                return View(context.Events);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;

                return View(context.Events.Where(Event => Event.CenterId == centerID));
            }

        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(int id)
        {
            Event Event;
            string[] roles = Roles.GetRolesForUser();
            if (roles.Any(item => item == "Administrators"))
            {
                Event = context.Events.Single(x => x.Id == id);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;
                Event = context.Events.Where(AEvent => AEvent.CenterId == centerID).Single(x => x.Id == id);
            }

            return View(Event);
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = context.Centers;
            return View();
        } 

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event Event)
        {
            if (ModelState.IsValid)
            {
                context.Events.Add(Event);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCenters = context.Centers;
            return View(Event);
        }
        
        //
        // GET: /Events/Edit/5
 
        public ActionResult Edit(int id)
        {
            Event Event;
            string[] roles = Roles.GetRolesForUser();
            if (roles.Any(item => item == "Administrators"))
            {
                Event = context.Events.Single(x => x.Id == id);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;
                Event = context.Events.Where(AEvent => AEvent.CenterId == centerID).Single(x => x.Id == id);
            }

            ViewBag.PossibleCenters = context.Centers;
            return View(Event);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event Event)
        {
            if (ModelState.IsValid)
            {
                context.Entry(Event).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleCenters = context.Centers;
            return View(Event);
        }

        //
        // GET: /Events/Delete/5
 
        public ActionResult Delete(int id)
        {
            Event Event;
            string[] roles = Roles.GetRolesForUser();
            if (roles.Any(item => item == "Administrators"))
            {
                Event = context.Events.Single(x => x.Id == id);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;
                Event = context.Events.Where(AEvent => AEvent.CenterId == centerID).Single(x => x.Id == id);
            }

            return View(Event);
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event Event;
            string[] roles = Roles.GetRolesForUser();
            if (roles.Any(item => item == "Administrators"))
            {
                Event = context.Events.Single(x => x.Id == id);
            }
            else
            {
                int centerID = AccountProfile.CurrentUser.CenterID;
                Event = context.Events.Where(AEvent => AEvent.CenterId == centerID).Single(x => x.Id == id);
            }

            context.Events.Remove(Event);
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
