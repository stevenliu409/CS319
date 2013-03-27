﻿using System;
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

        public ViewResult Index()
        {
            int centerID = account.GetCurrentUserCenterId();
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            var Events = repoService.eventRepo.FindAllWithCenterId(centerID);
            ViewBag.CurrentPage = "Events";
            return View(Events.ToList());
        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(int id)
        {         
            int centerID = account.GetCurrentUserCenterId();
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            Event anEvent = repoService.eventRepo.FindByIdAndCenterId(id, centerID);

            return View(anEvent);
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

            IEnumerable<Center> centers = repoService.centerRepo.FindListById(centerID);

            ViewBag.CenterId = new SelectList(centers, "Id", "Name");
           
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

            IEnumerable<Center> centers = repoService.centerRepo.FindListById(centerID);

            ViewBag.CenterId = new SelectList(centers, "Id", "Name");
           
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
