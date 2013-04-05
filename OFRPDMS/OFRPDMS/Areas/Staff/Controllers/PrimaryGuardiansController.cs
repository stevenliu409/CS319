using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using OFRPDMS.Account;
using OFRPDMS.Models;
using OFRPDMS.Repositories;
using System.Data.Objects;
using PagedList;

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class PrimaryGuardiansController : Controller
    {
        private IAccountService account;
        private IRepositoryService repoService;

        public PrimaryGuardiansController() {}

        public PrimaryGuardiansController(IAccountService account, IRepositoryService repoService)
        {
            this.account = account;
            this.repoService = repoService;
        }

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
            int centerId = account.GetCurrentUserCenterId();
            string[] roles = account.GetRolesForUser();
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

            var primaryguardians = from p in repoService.primaryGuardianRepo.FindAllWithCenterId(centerId)
                                   select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchStrings = searchString.Split(new char[] { ' ' });
                string[] searchFields = new string[] { "FirstName", "LastName", "Country", "Email", "Language", "Phone", "PostalCodePrefix", "Allergies", "DateCreated" };
                IEnumerable<PropertyInfo> properties = typeof(PrimaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));

                primaryguardians = primaryguardians.Where(
                    p => ( properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(searchString.ToUpper()))));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    primaryguardians = primaryguardians.OrderBy(p => p.FirstName);
                    
                    break;
                case "Name desc1":
                    
                    primaryguardians = primaryguardians.OrderBy(p => p.LastName);
                  
                    break;
                case "Name desc2":
           
                    primaryguardians = primaryguardians.OrderBy(p => p.Email);
               
                    break;
                case "Name desc3":
 
                    primaryguardians = primaryguardians.OrderBy(p => p.Country);
                    break;
                case "Name desc4":
                   
                    primaryguardians = primaryguardians.OrderBy(p => p.Language);
                   
                    break;
                case "Name desc5":
                    primaryguardians = primaryguardians.OrderBy(p => p.PostalCodePrefix);
                 
                    break;

                case "Name desc6":
                    primaryguardians = primaryguardians.OrderBy(p => p.Allergies);
                 
                    break;

                case "Name desc7":
                    primaryguardians = primaryguardians.OrderBy(p => p.Phone);

                    break;
                case "Name desc8":
                    primaryguardians = primaryguardians.OrderBy(p => p.Children.Count);

                    break;
                case "Name desc9":
                    primaryguardians = primaryguardians.OrderBy(p => p.SecondaryGuardians.Count);

                    break;

                case "Date":
                    primaryguardians = primaryguardians.OrderBy(s => s.DateCreated);
                    break;
                case "Date desc":
                     primaryguardians = primaryguardians.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    primaryguardians = primaryguardians.OrderBy(s => s.LastName);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(primaryguardians.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PrimaryGuardians/Details/5

        public ViewResult Details(int id)
        {
            int centerId = AccountProfile.CurrentUser.CenterID;
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            PrimaryGuardian primaryguardian = repoService.primaryGuardianRepo.FindByIdAndCenterId(id, centerId);
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Create

        public ActionResult Create()
        {
            ViewBag.CenterId2 = account.GetCurrentUserCenterId();
            var model = new PrimaryGuardian();
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

                }
                repoService.primaryGuardianRepo.Add(primaryguardian);
                return RedirectToAction("Index");
            }
            ViewBag.CenterId2 = AccountProfile.CurrentUser.CenterID;
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindAll(), "Id", "Name", primaryguardian.CenterId);
            return View(primaryguardian);
        }
        
        //
        // GET: /PrimaryGuardians/Edit/5
       
        public ActionResult Edit(int id)
        {
            int centerId = AccountProfile.CurrentUser.CenterID;
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            ViewBag.CenterId2 = centerId;
            PrimaryGuardian pr = repoService.primaryGuardianRepo.FindByIdAndCenterId(id, centerId);
           
            return View(pr);
        }

        //
        // POST: /PrimaryGuardians/Edit/5
       
        [HttpPost]
        public ActionResult Edit(PrimaryGuardian primaryguardian)
        {
            ViewBag.CenterId2 = AccountProfile.CurrentUser.CenterID;
              
            if (ModelState.IsValid)
            {

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
                            SecondaryGuardian sg = repoService.secondaryGuardianRepo.FindById(primaryguardian.SecondaryGuardians[i].Id);
                            repoService.secondaryGuardianRepo.Delete(sg);
                        }

                        primaryguardian.SecondaryGuardians.RemoveAt(i);
                    }

                    else if ((primaryguardian.SecondaryGuardians[i].Delete == true && (primaryguardian.SecondaryGuardians[i].Phone == null || primaryguardian.SecondaryGuardians[i].FirstName == null
                     || primaryguardian.SecondaryGuardians[i].LastName == null || primaryguardian.SecondaryGuardians[i].RelationshipToChild == null)) || (primaryguardian.SecondaryGuardians[i].Delete == true))
                    {
                        SecondaryGuardian sec = repoService.secondaryGuardianRepo.FindById(primaryguardian.SecondaryGuardians[i].Id);

                        if (sec != null)
                        {
                            repoService.secondaryGuardianRepo.Delete(sec);
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
                            repoService.secondaryGuardianRepo.Add(primaryguardian.SecondaryGuardians[i]);
                            primaryguardian.SecondaryGuardians.RemoveAt(i);
                        }
                        // existing secondary guardian was modified

                        else
                        {
                            repoService.secondaryGuardianRepo.Update(primaryguardian.SecondaryGuardians[i]);
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
                            Child child = repoService.childRepo.FindById(primaryguardian.Children[i].Id);
                            repoService.childRepo.Delete(child);
                        }

                        primaryguardian.Children.RemoveAt(i);
                    }
                  
                    else if ((primaryguardian.Children[i].Delete == true && (primaryguardian.Children[i].Birthdate == null || primaryguardian.Children[i].FirstName == null
                     || primaryguardian.Children[i].LastName == null || primaryguardian.Children[i].RelationshipToGuardian == null)) ||(primaryguardian.Children[i].Delete == true))
                    {
                        Child child = repoService.childRepo.FindById(primaryguardian.Children[i].Id);

                        if (child != null)
                        {
                            repoService.childRepo.Delete(child);
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
                            repoService.childRepo.Add(primaryguardian.Children[i]);
                            primaryguardian.Children.RemoveAt(i);
                        }
                        // existing child was modified
                        else
                        {
                            repoService.childRepo.Update(primaryguardian.Children[i]);
                        }
                    }
                }

            repoService.primaryGuardianRepo.Update(primaryguardian);

            return RedirectToAction("Index");
            }

            
            ViewBag.CenterId = new SelectList(repoService.centerRepo.FindAll(), "Id", "Name", primaryguardian.CenterId);
            return View(primaryguardian);
        }

        //
        // GET: /PrimaryGuardians/Delete/5

        public ActionResult Delete(int id)
        {
            string[] roles = Roles.GetRolesForUser();
            ViewBag.IsAdmin = roles.Contains("Administrators");
            PrimaryGuardian primaryguardian = repoService.primaryGuardianRepo.FindById(id);
            return View(primaryguardian);
        }

        //
        // POST: /PrimaryGuardians/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimaryGuardian primaryguardian = repoService.primaryGuardianRepo.FindById(id);
            repoService.primaryGuardianRepo.Delete(primaryguardian);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                repoService.primaryGuardianRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}