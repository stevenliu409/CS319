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
    public class CenterReferralsController : Controller
    {
        private OFRPDMSContext context = new OFRPDMSContext();

        //
        // GET: /CenterReferrals/

        public ViewResult Index()
        {

            return View();
        }

        //
        // GET: /CenterReferrals/Details/5

        public ViewResult Details(int id)
        {
            CenterReferral centerreferral = context.CenterReferrals.Single(x => x.Id == id);
            return View(centerreferral);
        }

        //
        // GET: /CenterReferrals/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = context.Centers;
            return View();
        } 

        //
        // POST: /CenterReferrals/Create

        [HttpPost]
        public ActionResult Create(CenterReferral centerreferral)
        {
            if (ModelState.IsValid)
            {
                centerreferral.CenterId = AccountProfile.CurrentUser.CenterID;
                context.CenterReferrals.Add(centerreferral);
                context.SaveChanges();
                return RedirectToAction("Edit");  
            }
            return View(centerreferral);
        }
        
        //
        // GET: /CenterReferrals/Edit/
 
        public ActionResult Edit()
        {
            ReferralViewModelsEdit referralsEdit = new ReferralViewModelsEdit();
            List<CenterReferral> referralsList =
                context.CenterReferrals.Where(referral => referral.CenterId ==
                    AccountProfile.CurrentUser.CenterID).ToList();
            List<ReferralViewModel> referralViewModelList = new List<ReferralViewModel>();

            foreach (var referral in referralsList)
            {
                ReferralViewModel r = new ReferralViewModel();
                r.referral = referral;
                r.count = 0;
                List<Referral> recordsOfReferralsMade = referral.Referrals.ToList();
                foreach (var record in recordsOfReferralsMade)
                {
                    r.totalNumberMade += record.CountReferred;
                }
                referralViewModelList.Add(r);
            }
            referralsEdit.Referrals = referralViewModelList;
            ViewBag.CurrentPage = "Referrals";
            return View(referralsEdit);
        }

        //
        // POST: /CenterReferrals/Edit/5

        [HttpPost]
        public ActionResult Edit(ReferralViewModelsEdit referralsEdit)
        {
            foreach (var item in referralsEdit.Referrals)
            {
                if (ModelState.IsValid)
                {
                    if (item.count > 0)
                    {
                        Referral r = new Referral();
                        r.CountReferred = item.count;
                        r.CenterReferralId = item.referral.Id;
                        context.Referrals.Add(r);
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /CenterReferrals/Delete/5
 
        public ActionResult Delete(int id)
        {
            CenterReferral centerreferral = context.CenterReferrals.Single(x => x.Id == id);
            return View(centerreferral);
        }

        //
        // POST: /CenterReferrals/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CenterReferral centerreferral = context.CenterReferrals.Single(x => x.Id == id);
            context.CenterReferrals.Remove(centerreferral);
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