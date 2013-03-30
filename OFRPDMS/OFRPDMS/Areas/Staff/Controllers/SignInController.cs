using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using PagedList;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class SignInController : Controller
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        //
        // GET: /Staff/SignIn/
        public ViewResult Index(int id=0) {
            int centerid = AccountProfile.CurrentUser.CenterID;
            var _ep = db.EventParticipants.Where(ep => ep.EventId == id && centerid == ep.Event.CenterId).ToList();
             
             return View(_ep);
        }

        [HttpPost,ActionName("Index")]
        public ActionResult ReIndex(int id)
        {
            int centerid = AccountProfile.CurrentUser.CenterID;
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
        public ActionResult Search(string name, string type, int? page) {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (type == "Primary")
            {
                var _primaryguardian = db.PrimaryGuardians.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name) || 
                    p.Allergies.Contains(name) || p.Country.Contains(name) || p.Language.Contains(name) || p.Email.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    email = pm.Email,
                    phone = pm.Phone,
                    prefix = pm.PostalCodePrefix,
                    datacreate = pm.DateCreated.ToString("MM/dd/yyyy"),
                    lang = pm.Language,
                    country = pm.Country,
                    allergy = pm.Allergies,
                    type = 1

                });
                return Json(collection.ToPagedList(pageNumber, pageSize), JsonRequestBehavior.AllowGet);
            }
            else if (type == "Child")
            {
                var _primaryguardian = db.Children.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name) || c.Allergies.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    relationshiptoGuardian = pm.PrimaryGuardian.FirstName,
                    allergy = pm.Allergies,
                    type = 3

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var _primaryguardian = db.SecondaryGuardians.Where(s => s.FirstName.Contains(name) || s.RelationshipToChild.Contains(name) || s.LastName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    relationship = pm.RelationshipToChild,
                    phone = pm.Phone,
                    relationshiptoGuardian = db.PrimaryGuardians.Find(pm.PrimaryGuardianId).FirstName,
                    type = 2

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }

        public void Add(int id, int type, int eventid)
        {
            if (type == 1)
            {
                var _primaryguardian = db.PrimaryGuardians.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.PrimaryGuardianId = _primaryguardian.Id;
                ep.ParticipantType = "Primary";
                ep.EventId = eventid;
                db.EventParticipants.Add(ep);
                db.SaveChanges();
            }
            else if (type == 3)
            {
                var _child = db.Children.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.ChildId = _child.Id;
                ep.ParticipantType = "Child";
                ep.EventId = eventid;
                db.EventParticipants.Add(ep);
                db.SaveChanges();
            }
            else {
                var _secondaryguardian = db.SecondaryGuardians.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.SecondaryGuardianId = _secondaryguardian.Id;
                ep.ParticipantType = "Secondary";
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