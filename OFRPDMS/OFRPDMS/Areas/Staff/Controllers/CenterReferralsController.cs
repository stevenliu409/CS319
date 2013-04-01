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
using OFRPDMS.Repositories;
using OFRPDMS.Account;
using Ninject;
using System.Reflection;

namespace OFRPDMS.Areas.Staff.Controllers
{   
    public class CenterReferralsController : Controller
    {

        private IRepositoryService repoService;
        private IAccountService account;

        public CenterReferralsController() { }

        [Inject]
        public CenterReferralsController(IAccountService account, IRepositoryService repoService) 
        {
            this.account = account;
            this.repoService = repoService;
        }
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
            CenterReferral centerreferral = repoService.centerReferralRepo.FindById(id);
            return View(centerreferral);
        }

        //
        // GET: /CenterReferrals/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCenters = repoService.centerRepo.FindAll();
            return View();
        } 

        //
        // POST: /CenterReferrals/Create

        [HttpPost]
        public ActionResult Create(CenterReferral centerreferral)
        {
            int centerId = account.GetCurrentUserCenterId();
            if (ModelState.IsValid)
            {
                centerreferral.CenterId = centerId;
                repoService.centerReferralRepo.Insert(centerreferral);
                return RedirectToAction("Edit");  
            }
            return View(centerreferral);
        }
        
        //
        // GET: /CenterReferrals/Edit/
 
        public ActionResult Edit()
        {
            int centerId = account.GetCurrentUserCenterId();
            ReferralViewModelsEdit referralsEdit = new ReferralViewModelsEdit();
            List<CenterReferral> referralsList = repoService.centerReferralRepo.FindAllWithCenterId(centerId).ToList();
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
            OFRPDMSContext context = new OFRPDMSContext();
            foreach (var item in referralsEdit.Referrals)
            {
                if (ModelState.IsValid)
                {
                    if (item.count > 0)
                    {
                        Referral r = new Referral();
                        r.CountReferred = item.count;
                        r.CenterReferralId = item.referral.Id;
                        repoService.centerReferralRepo.Insert(r);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /CenterReferrals/Delete/5
 
        public ActionResult Delete(int id)
        {
            CenterReferral centerreferral = repoService.centerReferralRepo.FindById(id);
            return View(centerreferral);
        }

        //
        // POST: /CenterReferrals/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CenterReferral centerreferral = repoService.centerReferralRepo.FindById(id);
            repoService.centerReferralRepo.Delete(centerreferral);
            return RedirectToAction("Edit");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                repoService.centerReferralRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}