using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OFRPDMS.Account;
using OFRPDMS.Models;
using OFRPDMS.Repositories;
using PagedList;

using Ninject;

namespace OFRPDMS.Areas.Staff.Controllers
{
    public class EventsController : Controller
    {
        private IRepositoryService repoService;
        private IAccountService account;

        public EventsController() {}

        [Inject]
        public EventsController(IAccountService account, IRepositoryService repoService)
        {
            this.account = account;
            this.repoService = repoService;
        }

        //
        // GET: /Events/

        public ViewResult Index(string sortOrder, int? page)
        {
            int centerID = account.GetCurrentUserCenterId();
            string[] roles = account.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            ViewBag.CurrentPage = "Events";

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            ViewBag.CenterNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            var events = from e in repoService.eventRepo.FindAllWithCenterId(centerID)
                           select e;

            switch (sortOrder)
            {
                case "Name desc":
                    events = events.OrderBy(e => e.Center);

                    break;
               
                case "Date":
                    events = events.OrderBy(e => e.Date);
                    break;
                case "Date desc":
                    events = events.OrderByDescending(e => e.Date);
                    break;
                default:
                    events = events.OrderBy(e => e.Date);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(events.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            int centerID = account.GetCurrentUserCenterId();

            IEnumerable<Center> centers = repoService.centerRepo.FindListById(centerID);

            ViewBag.CenterId = new SelectList(centers, "Id", "Name");

            return View();
        } 

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                anEvent.CenterId = account.GetCurrentUserCenterId();
                repoService.eventRepo.Insert(anEvent);
                return RedirectToAction("Index");  
            }
            return View(anEvent);
        }
        
        //
        // GET: /Events/Edit/5
 
        public ActionResult Edit(int id)
        {

            Event anEvent;
            int centerID = account.GetCurrentUserCenterId();

            anEvent = repoService.eventRepo.FindById(id);

           
            return View(anEvent);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event anEvent)
        {
            if (ModelState.IsValid)
            {
                anEvent.CenterId = account.GetCurrentUserCenterId();
                repoService.eventRepo.Update(anEvent);
                return RedirectToAction("Index");
            }
            int centerID = account.GetCurrentUserCenterId();
           
            return View(anEvent);
        }

        //
        // GET: /Events/Delete/5
 
        public ActionResult Delete(int id)
        {
            int centerID = account.GetCurrentUserCenterId();

            Event anEvent = repoService.eventRepo.FindByIdAndCenterId(id, centerID);

            return View(anEvent);          
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event anEvent = repoService.eventRepo.FindById(id);
            repoService.eventRepo.Delete(anEvent);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            repoService.eventRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
