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
    public class SignInController : Controller
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        //
        // GET: /Staff/SignIn/
        public ViewResult Index(int id=0) {
            int centerid = AccountProfile.CurrentUser.CenterID;
            // var eventparticipants = db.EventParticipants.Include(e => e.Event);
            var _ep = db.EventParticipants.Where(ep => ep.EventId == id).ToList();
             
             return View(_ep);
        }

        [HttpPost,ActionName("Index")]
        public ActionResult ReIndex(int id)
        {
            int centerid = AccountProfile.CurrentUser.CenterID;
           // var eventparticipants = db.EventParticipants.Include(e => e.Event);
            var _ep = db.EventParticipants.Where(ep => ep.EventId == id).ToList();
            return RedirectToRoute("Staff_default", new { centerIdArg = centerid, controller = "SignIn", action = "Index" , id = id});
        }

        //
        // GET: /Staff/SignIn/Details/5

        public ViewResult Details(int id)
        {
            EventParticipant eventparticipant = db.EventParticipants.Find(id);
            return View(eventparticipant);
        }

        //
        // GET: /Staff/SignIn/Create

        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Id");
            return View();
        } 

        //
        // POST: /Staff/SignIn/Create

        //[HttpPost]
        //public ActionResult Create(EventParticipant eventparticipant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.EventParticipants.Add(eventparticipant);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EventId = new SelectList(db.Events, "Id", "Id", eventparticipant.EventId);
        //    return View(eventparticipant);
        //    //return Search(eventparticipant);
        //}
        
        //
        // GET: /Staff/SignIn/Edit/5
 
        public ActionResult Edit(int id)
        {
            EventParticipant eventparticipant = db.EventParticipants.Find(id);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Id", eventparticipant.EventId);
            return View(eventparticipant);
        }

        //
        // POST: /Staff/SignIn/Edit/5

        [HttpPost]
        public ActionResult Edit(EventParticipant eventparticipant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventparticipant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Id", eventparticipant.EventId);
            return View(eventparticipant);
        }

        //
        // GET: /Staff/SignIn/Delete/5
 
        public ActionResult Delete(int id)
        {
            EventParticipant eventparticipant = db.EventParticipants.Find(id);
            return View(eventparticipant);
        }

        //
        // POST: /Staff/SignIn/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EventParticipant eventparticipant = db.EventParticipants.Find(id);
            db.EventParticipants.Remove(eventparticipant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Search(string name, string type) {
            if (type == "Primary") {         
                var _primaryguardian = db.PrimaryGuardians.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    email = pm.Email,
                    phone = pm.Phone,
                    prefix = pm.PostalCodePrefix,
                    datacreate = pm.DateCreated.ToString(),
                    lang = pm.Language,
                    country = pm.Country,


                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else if (type == "Child")
            {
                var _primaryguardian = db.Children.Where(c => c.FirstName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var _primaryguardian = db.SecondaryGuardians.Where(s => s.FirstName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }

        public void Add(int id, string type, int eventid)
        {
            if (type == "Primary")
            {
                var _primaryguardian = db.PrimaryGuardians.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.PrimaryGuardianId = _primaryguardian.Id;
                ep.ParticipantType = type;
                ep.EventId = eventid;
                db.EventParticipants.Add(ep);
                db.SaveChanges();
            }
            else if (type == "Child")
            {
                var _child = db.Children.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.ChildId = _child.Id;
                ep.ParticipantType = type;
                ep.EventId = eventid;
                db.EventParticipants.Add(ep);
                db.SaveChanges();
            }
            else {
                var _secondaryguardian = db.SecondaryGuardians.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.SecondaryGuardianId = _secondaryguardian.Id;
                ep.ParticipantType = type;
                ep.EventId = eventid;
                db.EventParticipants.Add(ep);
                db.SaveChanges();
            }

        }
        [HttpPost]
        public ActionResult findEvent() {
            int centerID = AccountProfile.CurrentUser.CenterID;
            var _events = db.Events.Where(e => e.CenterId == centerID).OrderByDescending(e=>e.Date).ToList();
            var collection = _events.Select(e => new
            {

                id = e.Id,
                date = e.Date.ToString("MM/dd/yyyy"),
 
            });
            return Json(collection, JsonRequestBehavior.AllowGet);
        }

    }
}