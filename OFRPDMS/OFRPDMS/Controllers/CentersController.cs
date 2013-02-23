using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

namespace OFRPDMS.Controllers
{   
    public class CentersController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /Centers/

        public ViewResult Index()
        {
            return View(context.Centers.Include(center => center.BorrowableItems).Include(center => center.Events).Include(center => center.CenterReferrals).Include(center => center.CenterAccounts).Include(center => center.CenterFreeResources).ToList());
        }

        //
        // GET: /Centers/Details/5

        public ViewResult Details(int id)
        {
            Center center = context.Centers.Single(x => x.Id == id);
            return View(center);
        }

        //
        // GET: /Centers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Centers/Create

        [HttpPost]
        public ActionResult Create(Center center)
        {
            if (ModelState.IsValid)
            {
                context.Centers.Add(center);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(center);
        }
        
        //
        // GET: /Centers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Center center = context.Centers.Single(x => x.Id == id);
            return View(center);
        }

        //
        // POST: /Centers/Edit/5

        [HttpPost]
        public ActionResult Edit(Center center)
        {
            if (ModelState.IsValid)
            {
                context.Entry(center).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(center);
        }

        //
        // GET: /Centers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Center center = context.Centers.Single(x => x.Id == id);
            return View(center);
        }

        //
        // POST: /Centers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Center center = context.Centers.Single(x => x.Id == id);
            context.Centers.Remove(center);
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