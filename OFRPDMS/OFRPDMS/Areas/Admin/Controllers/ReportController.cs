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
        ReportDBContext db = new ReportDBContext();

        DateTime startDay = new DateTime();
        DateTime endDay = new DateTime();

        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View(new Report());
        }

        //
        // GET: /Report/Generate

        public ActionResult Generate(Report report)
        {
            ViewBag.numOfNewPG = getNumOfNewPGTable();
            ViewBag.numOfPG = getNumOfPGTable();
            ViewBag.myReport = db.Report.First();
            return View(context.Centers);
        }

        //
        // Post: /Index

        [HttpPost]
        public ActionResult Index(Report report)
        {
            foreach (var entity in db.Report)
                db.Report.Remove(entity);
            db.SaveChanges();

            db.Report.Add(report);
            db.SaveChanges();
            return RedirectToAction("Generate","Report");
        }

        // get the number of new PG(created after start date) 
        private int[] getNumOfNewPGTable()
        {
            int[] newPGTable = new int[5];
            DateTime sday = db.Report.First().startDay;
            DateTime eday = db.Report.First().endDay;
            foreach (var pg in context.PrimaryGuardians)
            {
                //created later than start day and earlier than end day
                if (DateTime.Compare(pg.DateCreated, sday) > 0 && DateTime.Compare(pg.DateCreated, eday) < 0)
                {
                    newPGTable[pg.CenterId]++;
                }
                
            }
            return newPGTable;
        }

        private int[] getNumOfPGTable()
        {
            int[] pgTable = new int[5];
            DateTime eday = db.Report.First().endDay;
            foreach(var pg in context.PrimaryGuardians)
            {
                if (DateTime.Compare(pg.DateCreated, eday) < 0)
                pgTable[pg.CenterId]++;
            }
            return pgTable;
        }

        
    }
}

