using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.Models;
using OFRPDMS.Areas.Staff.ViewModels;
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;
using System.Reflection;


namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class CenterFreeResourcesController : Controller
    {
        private IRepositoryService repoService;
        private IAccountService account;

        public CenterFreeResourcesController() { }

        [Inject]
        public CenterFreeResourcesController(IAccountService account, IRepositoryService repoService) 
        {
            this.account = account;
            this.repoService = repoService;
        }

        //
        // GET: /CenterFreeResources/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /CenterFreeResources/Details/5

        public ViewResult Details(int id)
        {
            CenterFreeResource centerfreeresource = repoService.centerResourcesRepo.FindById(id);
            return View(centerfreeresource);
        }

        //
        // GET: /CenterFreeResources/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = repoService.centerRepo.FindAll();
            return View();
        } 

        //
        // POST: /CenterFreeResources/Create

        [HttpPost]
        public ActionResult Create(CenterFreeResource centerfreeresource)
        {
            int centerid = account.GetCurrentUserCenterId();
            if (ModelState.IsValid)
            {
                centerfreeresource.CenterId = centerid;
                repoService.centerResourcesRepo.Insert(centerfreeresource);
                return RedirectToAction("Edit");  
            }

            ViewBag.PossibleCenters = repoService.centerRepo.FindAll();
            return View(centerfreeresource);
        }
        
        //
        // GET: /CenterFreeResources/Edit
 
        public ActionResult Edit()
        {
            int centerid = account.GetCurrentUserCenterId();
            ResourceViewModelsEdit resourcesEdit = new ResourceViewModelsEdit();
            List<CenterFreeResource> resourceList = repoService.centerResourcesRepo.FindAllWithCenterId(centerid).ToList();
            List<ResourceViewModel> resouceViewModelList = new List<ResourceViewModel>();
            
            foreach (var resource in resourceList)
            {
                ResourceViewModel r = new ResourceViewModel();
                r.resource = resource;
                r.count = (int)resource.NumberAvailable;
                List<GivenResource> recordsOfGiven = resource.GivenResources.ToList();
                foreach (var record in recordsOfGiven)
                {
                    r.totalHandedOut += record.Count;
                }
                resouceViewModelList.Add(r);
            }
            resourcesEdit.Resources = resouceViewModelList;
            ViewBag.CurrentPage = "Resources";
            return View(resourcesEdit);
        }

        //
        // POST: /CenterFreeResources/Edit

        [HttpPost]
        public ActionResult Edit(ResourceViewModelsEdit resourcesEdit)
        {
            foreach (var item in resourcesEdit.Resources)
            {
                if (ModelState.IsValid)
                {
                    int handedOut = (int)(item.resource.NumberAvailable) - item.count;
                    int newStock = item.count;
                    //this if block allows the user to enter a negative number to indicate 
                    //number handed out instead of current stock
                    if (item.count < 0)
                    {
                        handedOut = -item.count;
                        if((int)(item.resource.NumberAvailable) - handedOut < 0)
                            handedOut = (int)(item.resource.NumberAvailable);
                        newStock = (int)(item.resource.NumberAvailable) - handedOut;

                    }

                    CenterFreeResource aResource =  repoService.centerResourcesRepo.FindById(item.resource.Id);
                    aResource.NumberAvailable = newStock;
                    repoService.centerResourcesRepo.Update(aResource);
                    if (handedOut > 0)
                    {
                        GivenResource gr = new GivenResource();
                        gr.Count = handedOut;
                        gr.CenterFreeResourceId = item.resource.Id;
                        repoService.centerResourcesRepo.Insert(gr);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /CenterFreeResources/Delete/5
 
        public ActionResult Delete(int id)
        {
            CenterFreeResource centerfreeresource = repoService.centerResourcesRepo.FindById(id);
            return View(centerfreeresource);
        }

        //
        // POST: /CenterFreeResources/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CenterFreeResource centerfreeresource = repoService.centerResourcesRepo.FindById(id);
            repoService.centerResourcesRepo.Delete(centerfreeresource);
            return RedirectToAction("Edit");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                repoService.centerResourcesRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}