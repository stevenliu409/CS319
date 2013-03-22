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
            //model.BuildEntity(1);
            //model.Children.Add(new Child());
            
            return View(model);
        } 

        //
        // POST: /PrimaryGuardians/Create

        [HttpPost]
        public ActionResult Create(PrimaryGuardian primaryguardian)
        {


            if (ModelState.IsValid)
            {
                primaryguardian.DateCreated = DateTime.Now;
                int x = primaryguardian.SecondaryGuardians.Count();
                for (var i = x-1; i >= 0; i--)
                {
                    if (primaryguardian.SecondaryGuardians[i].Delete == true || (primaryguardian.SecondaryGuardians[i].RelationshipToChild == null && primaryguardian.SecondaryGuardians[i].FirstName == null
                        && primaryguardian.SecondaryGuardians[i].LastName == null))
                    {
                        primaryguardian.SecondaryGuardians.RemoveAt(i);
                    }
                   
                }
              /*  int z = primaryguardian.Allergies.Count();
                for (var i = z - 1; i >= 0; i--)
                {
                    if (primaryguardian.Allergies[i].Delete == true)
                    {
                        primaryguardian.Allergies.RemoveAt(i);
                    }

                }*/

                int y = primaryguardian.Children.Count();
                for (int i = y -1; i >= 0; i--)
                {
                    if (primaryguardian.Children[i].Delete == true || (primaryguardian.Children[i].Birthdate == null && primaryguardian.Children[i].FirstName ==null
                        && primaryguardian.Children[i].LastName == null && primaryguardian.Children[i].RelationshipToGuardian ==null))
                    {
                        primaryguardian.Children.RemoveAt(i);
                    }
                  
                    //int z =primaryguardian.Children[i].Allergies.Count();
                    //for (int j = z - 1; j >= 0; j--)
                   // {
                        //if (primaryguardian.Children[i].Allergies[j].Delete == true)
                        //{
                      //      primaryguardian.Children[i].Allergies.RemoveAt(j);
                      //  }
                    //}
                }

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
                
                primaryguardian.DateCreated = DateTime.Now;

                int x = primaryguardian.SecondaryGuardians.Count();
                for (var i = x - 1; i >= 0; i--)
                {
                    if (primaryguardian.SecondaryGuardians[i].Delete == true || (primaryguardian.SecondaryGuardians[i].RelationshipToChild == null && primaryguardian.SecondaryGuardians[i].FirstName == null
                        && primaryguardian.SecondaryGuardians[i].LastName == null))
                    {
                        primaryguardian.SecondaryGuardians.RemoveAt(i);
                    }
                   
                }
               /* int z = primaryguardian.Allergies.Count();
                for (var i = z - 1; i >= 0; i--)
                {
                    if (primaryguardian.Allergies[i].Delete == true)
                    {
                        primaryguardian.Allergies.RemoveAt(i);
                    }

                }*/


                int y = primaryguardian.Children.Count();
                for (var i = y - 1; i >= 0; i--)
                {
                    if (primaryguardian.Children[i].Delete == true|| (primaryguardian.Children[i].Birthdate == null && primaryguardian.Children[i].FirstName ==null
                        && primaryguardian.Children[i].LastName == null && primaryguardian.Children[i].RelationshipToGuardian ==null))
                    {
                        primaryguardian.Children.RemoveAt(i);
                    }
                    //int z = primaryguardian.Children[i].Allergies.Count();
                    //for (var j = z - 1; j >= 0; j--)
                    //{
                        //if (primaryguardian.Children[i].Allergies[j].Delete == true)
                        //{
                        //    primaryguardian.Children[i].Allergies.RemoveAt(j);
                      //  }
                    //}
                }
                PrimaryGuardian pr = context.PrimaryGuardians.Find(primaryguardian.Id);
                context.PrimaryGuardians.Remove(pr);
                context.PrimaryGuardians.Add(primaryguardian);
               
                
                
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