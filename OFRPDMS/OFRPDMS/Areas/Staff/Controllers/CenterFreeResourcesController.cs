using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.Models;

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
                context.CenterFreeResources.Add(centerfreeresource);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCenters = context.Centers;
            return View(centerfreeresource);
        }
        
        //
        // GET: /CenterFreeResources/Edit/5
 
        public ActionResult Edit()
        {
            CenterFreeResourcesEdit centerfreeresourcesEdit = new CenterFreeResourcesEdit();
            List<CenterFreeResource> resourceList = context.CenterFreeResources.ToList();
            centerfreeresourcesEdit.CenterFreeResources = resourceList;
            return View(centerfreeresourcesEdit);
        }

        //
        // POST: /CenterFreeResources/Edit/5

        [HttpPost]
        public ActionResult Edit(CenterFreeResourcesEdit centerfreeresource)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Entry(centerfreeresource).State = EntityState.Modified;
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            ViewBag.PossibleCenters = context.Centers;
            return View(centerfreeresource);
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