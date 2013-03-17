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
    public class PrimaryGuardiansController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /PrimaryGuardians/

        public ViewResult Index()
        {
            var primaryguardians = context.PrimaryGuardians.Include(p => p.Center);
            return View(primaryguardians.ToList());
        }

        //
        // GET: /PrimaryGuardians/Details/5

        public ViewResult Details(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Find(id);
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Create

        public ActionResult Create()
        {
            ViewBag.CenterId = new SelectList(context.Centers, "Id", "Name");
            var model = new PrimaryGuardian();
            model.BuildEntity(1);
            return View(model);
        } 

        //
        // POST: /PrimaryGuardians/Create

        [HttpPost]
        public ActionResult Create(PrimaryGuardian primaryguardian)
        {
            PrimaryGuardian pr = new PrimaryGuardian();
            if (ModelState.IsValid)
            {
                primaryguardian.DateCreated = DateTime.Now;
                context.PrimaryGuardians.Add(primaryguardian);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CenterId = new SelectList(context.Centers, "Id", "Name", primaryguardian.CenterId);
            return View(primaryguardian);
        }
        
        //
        // GET: /PrimaryGuardians/Edit/5

        public ActionResult Edit(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Find(id);
            ViewBag.CenterId = new SelectList(context.Centers, "Id", "Name", primaryguardian.CenterId);
           
            return View(primaryguardian);
        }

        //
        // POST: /PrimaryGuardians/Edit/5

        [HttpPost]
        public ActionResult Edit(PrimaryGuardian primaryguardian)
        {
            //PrimaryGuardian prim = new PrimaryGuardian();
            //prim = primaryguardian;

           
            
            if (ModelState.IsValid)
            {
                foreach (var child in primaryguardian.Children)
                {
                    child.Id = primaryguardian.Id;
                    

                }

                foreach (var allergy in primaryguardian.Allergies)
                {
                    allergy.Id = primaryguardian.Id;


                }

                foreach (var second in primaryguardian.SecondaryGuardians)
                {
                    second.Id = primaryguardian.Id;


                }
                primaryguardian.DateCreated = DateTime.Now;
                context.PrimaryGuardians.Add(primaryguardian);
                context.Entry(primaryguardian).State = EntityState.Modified;
                
                
                //context.Entry(prim).State = EntityState.Detached;
                //context.PrimaryGuardians.Remove(context.PrimaryGuardians.Find(primaryguardian.Id));
                //context.PrimaryGuardians.Add(prim);
                //context.Entry(prim).State = EntityState.Modified;
               // context.PrimaryGuardians.Add(primaryguardian);
                //context.Entry(primaryguardian).State = EntityState.Modified;
               
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(context.Centers, "Id", "Name", primaryguardian.CenterId);
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Delete/5

        public ActionResult Delete(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Find(id);
            return View(primaryguardian);
        }

        //
        // POST: /PrimaryGuardians/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Find(id);
            context.PrimaryGuardians.Remove(primaryguardian);
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