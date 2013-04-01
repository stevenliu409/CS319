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
using System.Reflection;

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
            if (type == "Primary")
            {
                var _primaryguardian = repoService.primaryGuardianRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Country", "Email", "Language", "Phone", "PostalCodePrefix", "Allergies", "DateCreated" };
                IEnumerable<PropertyInfo> properties = typeof(PrimaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
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
                var _primaryguardian = repoService.childRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Allergies"};
                IEnumerable<PropertyInfo> properties = typeof(Child).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
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
                var _primaryguardian = repoService.secondaryGuardianRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "RelationshipToChild" };
                IEnumerable<PropertyInfo> properties = typeof(SecondaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    relationship = pm.RelationshipToChild,
                    phone = pm.Phone,
                    relationshiptoGuardian = repoService.primaryGuardianRepo.FindById(pm.PrimaryGuardianId).FirstName,
                    type = 2

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }

        public void Add(int id, int type, DateTime eventdate)
        {
            int centerid = account.GetCurrentUserCenterId();
            var all_center_event = repoService.eventRepo.FindAllWithCenterId(centerid);
            var eve = all_center_event.Where(e => DateTime.Compare(e.Date, eventdate) == 0).SingleOrDefault();
            if (eve == null)
            {
                eve = new Event();
                eve.CenterId = account.GetCurrentUserCenterId();
                eve.Date = eventdate;
                repoService.eventRepo.Insert(eve);
            }
            EventParticipant ep = new EventParticipant();
            if (type == 1)
            {
                if (repoService.signInRepo.FindPrimaryGuardianByIdAndEventId(id, eve.Id).
                    SingleOrDefault() == null)
                {
                    var _primaryguardian = repoService.primaryGuardianRepo.FindById(id);
                    ep.EventId = eve.Id;
                    ep.PrimaryGuardianId = _primaryguardian.Id;
                    ep.ParticipantType = "Primary";
                    repoService.signInRepo.Insert(ep);
                }
                
            }
            else if (type == 3)
            {
                if (repoService.signInRepo.FindChildByIdAndEventId(id,eve.Id)
                    .SingleOrDefault() == null)
                {
                    var _child = repoService.childRepo.FindById(id);
                    ep.ChildId = _child.Id;
                    ep.ParticipantType = "Child";
                    ep.EventId = eve.Id;
                    repoService.signInRepo.Insert(ep);
                }
            }
            else
            {
                if (repoService.signInRepo.FindSecondaryGuardianByIdAndEventId(id, eve.Id).
                    SingleOrDefault() == null)
                {
                    var _secondaryguardian = repoService.secondaryGuardianRepo.FindById(id);
                    ep.SecondaryGuardianId = _secondaryguardian.Id;
                    ep.ParticipantType = "Secondary";
                    ep.EventId = eve.Id;
                    repoService.signInRepo.Insert(ep);
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