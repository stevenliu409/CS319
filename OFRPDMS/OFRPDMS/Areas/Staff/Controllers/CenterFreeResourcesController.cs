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

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class CenterFreeResourcesController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /CenterFreeResources/

        public ViewResult Index()
        {
            return View(context.CenterFreeResources.Include(centerfreeresource => centerfreeresource.Center).Include(centerfreeresource => centerfreeresource.GivenResources).ToList());
        }

        //
        // GET: /CenterFreeResources/Details/5

        public ViewResult Details(int id)
        {
            CenterFreeResource centerfreeresource = context.CenterFreeResources.Single(x => x.Id == id);
            return View(centerfreeresource);
        }

        //
        // GET: /CenterFreeResources/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = context.Centers;
            return View();
        } 

        //
        // POST: /CenterFreeResources/Create

        [HttpPost]
        public ActionResult Create(CenterFreeResource centerfreeresource)
        {
            if (ModelState.IsValid)
            {
                centerfreeresource.CenterId = AccountProfile.CurrentUser.CenterID;
                context.CenterFreeResources.Add(centerfreeresource);
                context.SaveChanges();
                return RedirectToAction("Edit");  
            }

            ViewBag.PossibleCenters = context.Centers;
            return View(centerfreeresource);
        }
        
        //
        // GET: /CenterFreeResources/Edit
 
        public ActionResult Edit()
        {
            ResourceViewModelsEdit resourcesEdit = new ResourceViewModelsEdit();
            List<CenterFreeResource> resourceList = 
                context.CenterFreeResources.Where(resource => resource.CenterId == 
                    AccountProfile.CurrentUser.CenterID).ToList();
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

                    CenterFreeResource aResource =  context.CenterFreeResources.Find(item.resource.Id);
                    aResource.NumberAvailable = newStock;
                    context.Entry(aResource).State = EntityState.Modified;
                    context.SaveChanges();
                    if (handedOut > 0)
                    {
                        GivenResource gr = new GivenResource();
                        gr.Count = handedOut;
                        gr.CenterFreeResourceId = item.resource.Id;
                        context.GivenResources.Add(gr);
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /CenterFreeResources/Delete/5
 
        public ActionResult Delete(int id)
        {
            CenterFreeResource centerfreeresource = context.CenterFreeResources.Single(x => x.Id == id);
            return View(centerfreeresource);
        }

        //
        // POST: /CenterFreeResources/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CenterFreeResource centerfreeresource = context.CenterFreeResources.Single(x => x.Id == id);
            context.CenterFreeResources.Remove(centerfreeresource);
            context.SaveChanges();
            return RedirectToAction("Edit");
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