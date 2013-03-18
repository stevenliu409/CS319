using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Controllers
{ 
    public class LibraryController : Controller
    {
        private string[] ValidTypes = new string[3] { "video", "book", "toy" };

        private OFRPDMSContext db = new OFRPDMSContext();

        //
        // GET: /Staff/Library/

        public ActionResult Index(int centerIdArg)
        {
            bool valid = VerifyCenterId(centerIdArg);

            // staff is trying to access center they're not associated with
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            if (centerIdArg == 0)
            {
                // administrator is viewing everything
                var libraryitems = db.LibraryResources.Include(l => l.Center);
                return View(libraryitems.ToList());
            }
            else
            {
                var libraryitems = db.LibraryResources.Include(l => l.Center).Where(l => l.CenterId == centerIdArg);
                return View(libraryitems.ToList());
            }
        }

        //
        // GET: /Staff/Library/Details/5

        public ActionResult Details(int centerIdArg, int id)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            LibraryResource libraryitem = db.LibraryResources.Find(id);
            return View(libraryitem);
        }

        //
        // GET: /Staff/Library/Create

        public ActionResult Create(int centerIdArg)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            ViewBag.CenterId = new SelectList(db.Centers.Where(c => c.Id == centerIdArg), "Id", "Name");
            ViewBag.ItemTypes = new SelectList(ValidTypes.AsEnumerable());
            return View();
        } 

        //
        // POST: /Staff/Library/Create

        [HttpPost]
        public ActionResult Create(int centerIdArg, LibraryResource libraryitem)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            if (ModelState.IsValid)
            {
                db.LibraryResources.Add(libraryitem);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ItemType = new SelectList(ValidTypes.AsEnumerable());
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }
        
        //
        // GET: /Staff/Library/Edit/5
 
        public ActionResult Edit(int centerIdArg, int id)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            LibraryResource libraryitem = db.LibraryResources.Find(id);
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }

        //
        // POST: /Staff/Library/Edit/5

        [HttpPost]
        public ActionResult Edit(int centerIdArg, LibraryResource libraryitem)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            if (ModelState.IsValid)
            {
                db.Entry(libraryitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", libraryitem.CenterId);
            return View(libraryitem);
        }

        //
        // GET: /Staff/Library/Delete/5
 
        public ActionResult Delete(int centerIdArg, int id)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            LibraryResource libraryitem = db.LibraryResources.Find(id);
            return View(libraryitem);
        }

        //
        // POST: /Staff/Library/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int centerIdArg, int id)
        {
            bool valid = VerifyCenterId(centerIdArg);
            if (!valid)
            {
                return RedirectToAction("Index", "Staff");
            }

            LibraryResource libraryitem = db.LibraryResources.Find(id);
            db.LibraryResources.Remove(libraryitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // verifies the given center id is valid for current user
        // then sets CenterId in the viewbag if valid
        private bool VerifyCenterId(int id)
        {
            
            string[] roles = Roles.GetRolesForUser();

            // Administrator
            if (roles.Any(item => item == "Administrators"))
            {
                ViewBag.StaffCenterId = id;
                return true; // always valid for administrator
            }
            // Staff
            else
            {
                int staffCenterId = AccountProfile.CurrentUser.CenterID;
                if (staffCenterId == id)
                {
                    ViewBag.StaffCenterId = id;
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "Insufficient privileges to access the center. Please request an account from the administrator.");
                    return false;
                }
            }
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
