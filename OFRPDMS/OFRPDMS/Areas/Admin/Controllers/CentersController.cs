using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;

// Master Branch
namespace OFRPDMS.Areas.Admin.Controllers
{   
    public class CentersController : Controller
    {
        OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /Centers/

        public ViewResult Index(string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.EmailNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc1" : "";
            ViewBag.AdressNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc2" : "";
            ViewBag.PhoneNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc3" : "";

            var center = from p in context.Centers
                         select p;

            switch (sortOrder)
            {
                case "Name desc":
                    center = center.OrderByDescending(p => p.Name);
                    
                    break;
                case "Name desc1":
                    
                    center = center.OrderByDescending(p => p.Address);
                  
                    break;
                case "Name desc2":
           
                    center = center.OrderByDescending(p => p.Email);
               
                    break;
                case "Name desc3":
 
                    center = center.OrderByDescending(p => p.Phone);
                    break;

                default:
                    center = center.OrderBy(s => s.Name);
                    break;
            }
            return View(center.ToList());
        }

        //
        // GET: /Centers/Details/5

        public ViewResult Details(int id)
        {
            Center center = context.Centers.Find(id);
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
            Center center = context.Centers.Find(id);
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
            Center center = context.Centers.Find(id);
            return View(center);
        }

        //
        // POST: /Centers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Center center = context.Centers.Find(id);
            IEnumerable<Event> centerevents = context.Events.Where(cid => cid.CenterId == id);
            foreach (var item in centerevents)
            {
                context.Events.Remove(item);
            }
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