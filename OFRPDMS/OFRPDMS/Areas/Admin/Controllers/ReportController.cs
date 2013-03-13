using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using OFRPDMS.Areas.Admin.Models;

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        OFRPDMSContext context = new OFRPDMSContext();
        Report myReport = new Report();
        int[,] pgTable = new int[2,0];
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Report/Generate

        public ActionResult Generate()
        {
            ViewBag.numOfNewPG = getNumOfNewPG();
            return View();
        }


        
        //
        // Post: /Index

        [HttpPost]
        public ActionResult Index(Report report)
        {
            myReport = report;
            return RedirectToAction("Generate","Report");
        }

        // get the number of new PG(created after start date) 
        private int getNumOfNewPG()
        {
            return (from u in context.PrimaryGuardians
                    where u.DateCreated > myReport.startDay
                    select u).ToArray().Length;
        }

        private int[,] createPGTable()
        {
            for (int i = 0; i < 2; i++)
            {

            }
                return pgTable;
        }
        
    }
}
