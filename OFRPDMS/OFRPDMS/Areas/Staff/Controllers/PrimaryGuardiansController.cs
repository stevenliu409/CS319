using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using OFRPDMS.Models;
using System.Data.Objects;
using PagedList;

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class PrimaryGuardiansController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /PrimaryGuardians/

        public ViewResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc1" : "";
            ViewBag.EmailNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc2" : "";
            ViewBag.CountryNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc3" : "";
            ViewBag.LanguageNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc4" : "";
            ViewBag.PostalcodeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc5" : "";
            ViewBag.AllergiesNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc6" : "";
            ViewBag.PhoneNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc7" : "";
            ViewBag.ChildrenNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc8" : "";
            ViewBag.SecondaryNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc9" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            int centerId = AccountProfile.CurrentUser.CenterID;
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");


            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var primaryguardian = from p in context.PrimaryGuardians.Where(p => p.CenterId == centerId).Include(p => p.Center)
                                   select p;
          
            if (!String.IsNullOrEmpty(searchString))
            {
     

                primaryguardian = primaryguardian.Where(p => p.LastName.ToUpper().Contains(searchString.ToUpper())
                           || p.FirstName.ToUpper().Contains(searchString.ToUpper()) || p.Country.ToUpper().Contains(searchString.ToUpper())
                            || p.Email.ToUpper().Contains(searchString.ToUpper()) 
                           || p.Language.ToUpper().Contains(searchString.ToUpper()) || p.Phone.ToUpper().Contains(searchString.ToUpper()) || p.PostalCodePrefix.ToUpper().Contains(searchString.ToUpper())
                           || p.Allergies.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.FirstName);
                    
                    break;
                case "Name desc1":
                    
                    primaryguardian = primaryguardian.OrderByDescending(p => p.LastName);
                  
                    break;
                case "Name desc2":
           
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Email);
               
                    break;
                case "Name desc3":
 
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Country);
                    break;
                case "Name desc4":
                   
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Language);
                   
                    break;
                case "Name desc5":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.PostalCodePrefix);
                 
                    break;

                case "Name desc6":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Allergies);
                 
                    break;

                case "Name desc7":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Phone);

                    break;
                case "Name desc8":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.Children.Count);

                    break;
                case "Name desc9":
                    primaryguardian = primaryguardian.OrderByDescending(p => p.SecondaryGuardians.Count);

                    break;

                case "Date":
                    primaryguardian = primaryguardian.OrderBy(s => s.DateCreated);
                    break;
                case "Date desc":
                     primaryguardian = primaryguardian.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    primaryguardian = primaryguardian.OrderBy(s => s.LastName);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(primaryguardian.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PrimaryGuardians/Details/5

        public ViewResult Details(int id)
        {
            int centerId = AccountProfile.CurrentUser.CenterID;
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            PrimaryGuardian primaryguardian = context.PrimaryGuardians.Where(p => p.CenterId == centerId && p.Id == id).SingleOrDefault();
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Create

        public ActionResult Create()
        {
            ViewBag.CenterId2 = AccountProfile.CurrentUser.CenterID;
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
                foreach (Child a in primaryguardian.Children)
                {
                    a.DateCreated = primaryguardian.DateCreated;
                }

                

                //Check for null field in the SecondaryGuardian , if all fields are null, then do not add to database
                //delete the element in the list which contains delete marked to "true"

                int x = primaryguardian.SecondaryGuardians.Count();
                for (var i = x-1; i >= 0; i--)
                {
                    if (primaryguardian.SecondaryGuardians[i].Delete == true || (primaryguardian.SecondaryGuardians[i].RelationshipToChild == null && primaryguardian.SecondaryGuardians[i].FirstName == null
                        && primaryguardian.SecondaryGuardians[i].LastName == null))
                    {
                        primaryguardian.SecondaryGuardians.RemoveAt(i);
                    }
                   
                }

                //Check for null field in the Allergies , if all fields are null, then do not add to database
                //delete the element in the list which contains delete marked to "true"

              /* int z = primaryguardian.Allergies.Count();
                for (var i = z - 1 ; i >= 0; i--)
                {
                    if (primaryguardian.Allergies[i].Delete == true || primaryguardian.Allergies[i].Note ==null)
                    {
                        primaryguardian.Allergies.RemoveAt(i);
                    }

                }*/

                //Check for null field in the Children , if all fields are null, then do not add to database
                //delete the element in the list which contains delete marked to "true"

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
       public static PrimaryGuardian pr;
        public ActionResult Edit(int id)
        {
            int centerId = AccountProfile.CurrentUser.CenterID;
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            ViewBag.CenterId2 = centerId;
            pr = context.PrimaryGuardians.Where(p => p.CenterId == centerId).Single(p => p.Id == id);
            
           
            return View(pr);
        }

        //
        // POST: /PrimaryGuardians/Edit/5
       
        [HttpPost]
        public ActionResult Edit(PrimaryGuardian primaryguardian)
        {
            
              
            if (ModelState.IsValid)
            {

                primaryguardian.DateCreated = pr.DateCreated;
                foreach (Child a in primaryguardian.Children)
                {
                    a.DateCreated = primaryguardian.DateCreated;
                }


                //Check for null field in the SecondaryGuardian , if all fields are null, then do not add to database
                //delete the element in the list which contains delete marked to "true"

                int x = primaryguardian.SecondaryGuardians.Count();
                for (var i = x - 1; i >= 0; i--)
                {
                    string[] includeFields = new string[] { "FirstName", "LastName", "RelationshipToChild", "Phone" };
                    IEnumerable<PropertyInfo> properties = typeof(SecondaryGuardian).GetProperties().Where(prop => includeFields.Contains(prop.Name));
                    bool validSG = false;

                    // if any input fields are not null, then the SecondaryGuardian is valid
                    validSG = properties.Any(
                        p => p.GetValue(primaryguardian.SecondaryGuardians[i], null) != null);

                    if (primaryguardian.SecondaryGuardians[i].Delete == true || !validSG)
                    {
                        if (primaryguardian.SecondaryGuardians[i].Id != 0)
                        {
                            SecondaryGuardian sg = context.SecondaryGuardians.Find(primaryguardian.SecondaryGuardians[i].Id);
                            context.SecondaryGuardians.Remove(sg);
                        }

                        primaryguardian.SecondaryGuardians.RemoveAt(i);
                    }

                    else if ((primaryguardian.SecondaryGuardians[i].Delete == true && (primaryguardian.SecondaryGuardians[i].Phone == null || primaryguardian.SecondaryGuardians[i].FirstName == null
                     || primaryguardian.SecondaryGuardians[i].LastName == null || primaryguardian.SecondaryGuardians[i].RelationshipToChild == null)) || (primaryguardian.SecondaryGuardians[i].Delete == true))
                    {
                        SecondaryGuardian sec = context.SecondaryGuardians.Find(primaryguardian.SecondaryGuardians[i].Id);

                        if (sec != null)
                        {

                            context.SecondaryGuardians.Remove(sec);
                            primaryguardian.SecondaryGuardians.RemoveAt(i);
                        }
                        else
                        {
                            primaryguardian.SecondaryGuardians.RemoveAt(i);
                        }

                    }



                    else
                    {
                        primaryguardian.SecondaryGuardians[i].PrimaryGuardianId = primaryguardian.Id;

                        // this is a newly created secondary guardian
                        if (primaryguardian.SecondaryGuardians[i].Id == 0)
                        {
                            context.SecondaryGuardians.Add(primaryguardian.SecondaryGuardians[i]);
                            primaryguardian.SecondaryGuardians.RemoveAt(i);
                        }
                        // existing secondary guardian was modified

                        else
                        {
                            context.Entry(primaryguardian.SecondaryGuardians[i]).State = EntityState.Modified;
                        }
                    }
                }

              
                //Check for null field in the Children , if all fields are null, then do not add to database
                //delete the element in the list which contains delete marked to "true"

                int y = primaryguardian.Children.Count();

                for (var i = y - 1; i >= 0; i--)
                {
                    string[] includeFields = new string[] { "FirstName", "LastName", "Birthdate", "RelationshipToGuardian" };
                    IEnumerable<PropertyInfo> properties = typeof(Child).GetProperties().Where(prop => includeFields.Contains(prop.Name));
                    bool validChild = false;
                    

                    // if any input fields are not null, then the SecondaryGuardian is valid
                    validChild = properties.Any(
                        p => p.GetValue(primaryguardian.Children[i], null) != null &&
                                        !includeFields.Contains(p.Name));

                    // child needs to be deleted
               
                    
                    if ((primaryguardian.Children[i].Birthdate == null && primaryguardian.Children[i].FirstName == null
                        && primaryguardian.Children[i].LastName == null && primaryguardian.Children[i].RelationshipToGuardian == null)
                        || (primaryguardian.Children[i].Delete == true && (primaryguardian.Children[i].Birthdate == null && primaryguardian.Children[i].FirstName == null
                        && primaryguardian.Children[i].LastName == null && primaryguardian.Children[i].RelationshipToGuardian == null)))
                    {

                        if (primaryguardian.Children[i].Id != 0)
                        {
                            Child child = context.Children.Find(primaryguardian.Children[i].Id);
                            context.Children.Remove(child);
                        }

                        primaryguardian.Children.RemoveAt(i);
                    }
                  
                    else if ((primaryguardian.Children[i].Delete == true && (primaryguardian.Children[i].Birthdate == null || primaryguardian.Children[i].FirstName == null
                     || primaryguardian.Children[i].LastName == null || primaryguardian.Children[i].RelationshipToGuardian == null)) ||(primaryguardian.Children[i].Delete == true))
                    {
                        Child child = context.Children.Find(primaryguardian.Children[i].Id);

                        if (child != null)
                        {

                            context.Children.Remove(child);
                            primaryguardian.Children.RemoveAt(i);
                        }
                        else
                        {
                            primaryguardian.Children.RemoveAt(i);
                        }       
                        
                    }

                    

                    else
                    {
                        primaryguardian.Children[i].PrimaryGuardianId = primaryguardian.Id;

                        // this is a newly created child
                        if (primaryguardian.Children[i].Id == 0)
                        {
                            context.Children.Add(primaryguardian.Children[i]);
                            primaryguardian.Children.RemoveAt(i);
                        }
                        // existing child was modified
                        else
                        {
                            context.Entry(primaryguardian.Children[i]).State = EntityState.Modified;
                        }
                    }
                }

                context.Entry(primaryguardian).State = EntityState.Modified;
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
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
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