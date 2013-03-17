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
        int[,] myArray = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

        
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
            ViewBag.numOfNewPG = getNumOfNewPGTable();
            ViewBag.numOfPG = getNumOfPGTable();
            return View(context.Centers);
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
        private int[,] getNumOfNewPGTable()
        {
            int[,] newPGTable = new int[5, 1];
            for (int i = 0; i < context.Centers.ToArray().Length; i++)
            {
                newPGTable[i,0] = (from u in context.PrimaryGuardians
                    where u.DateCreated > myReport.startDay && u.Center.Id==i
                    select u).ToArray().Length;
            }
            return newPGTable;
        }

        private int[,] getNumOfPGTable()
        {
            int[,] pgTable = new int[5, 1];
            for (int i = 0; i < context.Centers.ToArray().Length; i++)
            {
                pgTable[i, 0] = (from u in context.PrimaryGuardians
                                    where  u.Center.Id == i
                                    select u).ToArray().Length;
            }
            return pgTable;
        }
        
    }
}
