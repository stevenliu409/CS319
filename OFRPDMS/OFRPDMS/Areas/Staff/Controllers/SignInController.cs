using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using PagedList;
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class SignInController : Controller
    {
        

        private IRepositoryService repoService;
        private IAccountService account;

        public SignInController() { }
      
        [Inject]
        public SignInController(IAccountService account, IRepositoryService repoService) {

            this.account = account;
            this.repoService = repoService;
        }

        //
        // GET: /Staff/SignIn/
        public ViewResult Index(int? page,int id=0 ) {
            int centerid = account.GetCurrentUserCenterId();
            var _ep = repoService.signInRepo.FindByEventIdAndCenterId(id, centerid).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(_ep.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost,ActionName("Index")]
        public ActionResult ReIndex(int id)
        {
            int centerid = account.GetCurrentUserCenterId();
            var _ep = repoService.signInRepo.FindAllWithEventId(id).ToList();
            return RedirectToRoute("Staff_default", new { centerIdArg = centerid, controller = "SignIn", action = "Index" , id = id});
        }

        //
        // GET: /Staff/SignIn/Details/5

        public ViewResult Details(int id)
        {
            EventParticipant eventparticipant = repoService.signInRepo.FindById(id);
            return View(eventparticipant);
        }

        //
        // GET: /Staff/SignIn/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // GET: /Staff/SignIn/Delete/5
 
        public ActionResult Delete(int id)
        {
            EventParticipant eventparticipant = repoService.signInRepo.FindById(id);
            return View(eventparticipant);
        }

        //
        // POST: /Staff/SignIn/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EventParticipant eventparticipant = repoService.signInRepo.FindById(id);
            repoService.signInRepo.Delete(eventparticipant);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            repoService.signInRepo.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Search(string name, string type) {
            OFRPDMSContext db = new OFRPDMSContext();
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
                return Json(collection, JsonRequestBehavior.AllowGet);
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

        public void Add(int id, int type, DateTime eventid)
        {

            OFRPDMSContext db = new OFRPDMSContext();
            int centerid = account.GetCurrentUserCenterId();
            var eve = db.Events.Where(e => DateTime.Compare(e.Date, eventid) == 0 && e.CenterId == centerid).SingleOrDefault();
            if (eve == null)
            {
                eve = new Event();
                eve.CenterId = account.GetCurrentUserCenterId();
                eve.Date = eventid;
                db.Events.Add(eve);

            }
            EventParticipant ep = new EventParticipant();
            if (type == 1)
            {
                if(db.EventParticipants.Where(eps => eps.PrimaryGuardianId == id && eve.Id == eps.EventId).SingleOrDefault() == null){
                    var _primaryguardian = db.PrimaryGuardians.Find(id);
                    ep.EventId = eve.Id;
                    ep.PrimaryGuardianId = _primaryguardian.Id;
                    ep.ParticipantType = "Primary";
                    db.EventParticipants.Add(ep);
                    db.SaveChanges();
                }
                
            }
            else if (type == 3)
            {
                if (db.EventParticipants.Where(eps => eps.ChildId == id && eve.Id == eps.EventId).SingleOrDefault() == null)
                {
                    var _child = db.Children.Find(id);
                    ep.ChildId = _child.Id;
                    ep.ParticipantType = "Child";
                    ep.EventId = eve.Id;
                    db.EventParticipants.Add(ep);
                    db.SaveChanges();
                }
            }
            else
            {
                if (db.EventParticipants.Where(eps => eps.SecondaryGuardianId == id && eve.Id == eps.EventId).SingleOrDefault() == null)
                {
                    var _secondaryguardian = db.SecondaryGuardians.Find(id);
                    ep.SecondaryGuardianId = _secondaryguardian.Id;
                    ep.ParticipantType = "Secondary";
                    ep.EventId = eve.Id;
                    db.EventParticipants.Add(ep);
                    db.SaveChanges();
                }
            }

        }
        [HttpPost]
        public ActionResult findEvent() {
            int centerID = account.GetCurrentUserCenterId();
            var _events = repoService.eventRepo.FindAllWithCenterId(centerID).OrderByDescending(e=>e.Date).ToList();
            var collection = _events.Select(e => new
            {

                id = e.Id,
                date = e.Date.ToString("MM/dd/yyyy"),
 
            });
            return Json(collection, JsonRequestBehavior.AllowGet);
        }

    }
}