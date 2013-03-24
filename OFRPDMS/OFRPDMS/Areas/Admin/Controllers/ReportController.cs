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
        PGIDContext db = new PGIDContext();
        

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

        [HttpPost]
        public ActionResult Generate(Report report)
        {
            //ViewBag.myReport = db.Report.First();
            string[] disLanguage = getLanguages(report.startDay, report.endDay);
            string[] disCountry = getCountrys(report.startDay, report.endDay);
            ViewBag.myReport = report;
            ViewBag.numOfNewPG = getNumOfNewPGTable(report.startDay, report.endDay);
            ViewBag.numOfPG = getNumOfPGTable(report.endDay);
            ViewBag.distinctLanguage = disLanguage;
            ViewBag.distinctCountry = disCountry;
            ViewBag.center = context.Centers.ToArray();
            ViewBag.languageTable = getLanguageTable(disLanguage, report.startDay, report.endDay);
            ViewBag.countryTable = getCountryTable(disCountry,report.startDay, report.endDay);
            return View(context.Centers);
            
        }

        //
        // GET: /Report/Generate

        [HttpPost]
        public ActionResult TrackPG(Report report)
        {
            int name = db.pgids.First().pgid;
            return View();
        }

        //
        // Post: /Index

        [HttpPost]
        public ActionResult Index(Report report)
        {

            startDay = report.startDay;
            endDay = report.endDay;


            ViewBag.myReport = report;
            ViewBag.numOfNewPG = getNumOfNewPGTable(startDay, endDay);


            return RedirectToAction("Generate","Report");
        }

        [HttpPost]
        public ActionResult Search(string name, string type)
        {
            if (type == "Primary")
            {
                var _primaryguardian = context.PrimaryGuardians.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,
                    email = pm.Email,
                    phone = pm.Phone,
                    prefix = pm.PostalCodePrefix,
                    datacreate = pm.DateCreated.ToString(),
                    lang = pm.Language,
                    country = pm.Country,


                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else if (type == "Child")
            {
                var _primaryguardian = context.Children.Where(c => c.FirstName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var _primaryguardian = context.SecondaryGuardians.Where(s => s.FirstName.Contains(name)).ToList();
                var collection = _primaryguardian.Select(pm => new
                {

                    id = pm.Id,
                    Fname = pm.FirstName,
                    Lname = pm.LastName,

                });
                return Json(collection, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Add(int id, string type)
        {
            if (type == "Primary")
            {
                PGID mypgid = new PGID();

                
                foreach (var entity in db.pgids)
                    db.pgids.Remove(entity);
                db.SaveChanges();
                

                mypgid.pgid = id;
                db.pgids.Add(mypgid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (type == "Child")
            {
                var _child = context.Children.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.ParticipantId = _child.Id;
                ep.ParticipantType = type;
                context.EventParticipants.Add(ep);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                var _secondaryguardian = context.SecondaryGuardians.Find(id);
                EventParticipant ep = new EventParticipant();
                ep.ParticipantId = _secondaryguardian.Id;
                ep.ParticipantType = type;
                context.EventParticipants.Add(ep);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        // get the number of new PG(created after start date) 
        private int[] getNumOfNewPGTable(DateTime sday, DateTime eday)
        {
            int[] newPGTable = new int[10];

            foreach (var c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0 && DateTime.Compare(pg.DateCreated, eday) < 0 && pg.CenterId == c.Id);
                newPGTable[c.Id] = pgs.Count();
            }

            return newPGTable;
        }

        //private int[] getNumOfChild(DateTime eday)
        //{
        //    int[] pgTable = new int[10];
            
        //    foreach (var c in context.Centers)
        //    {
        //        IEnumerable<PrimaryGuardian> chs = context.Children.Where(ch => DateTime.Compare(ch., eday) < 0 && pg.CenterId == c.Id);
        //        IEnumerable<PrimaryGuardian> distinctPG = context.PrimaryGuardians.Distinct();
        //        pgTable[c.Id] = pgs.Count();
        //    }

        //    return pgTable;
        //}

        private int[] getNumOfPGTable(DateTime eday)
        {
            int[] pgTable = new int[10];

            foreach (var c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, eday) < 0 && pg.CenterId == c.Id);
                IEnumerable<PrimaryGuardian> distinctPG = context.PrimaryGuardians.Distinct();
                pgTable[c.Id] = pgs.Count();
            }

            return pgTable;
        }

        //private int getNumOfSignIn(DateTime sday, DateTime eday)
        //{
        //    IEnumerable<Event> events = context.Events.Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) < 0);
        //    foreach(Event evt in events)
        //    {
                
        //    }

        //}

        private string[] getLanguages(DateTime sday, DateTime eday)
        {
            IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                && DateTime.Compare(pg.DateCreated, eday) < 0 );
            List<string> language = new List<string>();
            
            
            foreach (var pg in pgs)
            {
                language.Add(pg.Language);
            }

            IEnumerable<string> distinctLanguage = language.Distinct();
            language = distinctLanguage.ToList();
            return distinctLanguage.ToArray();
        }

        private string[] getCountrys(DateTime sday, DateTime eday)
        {
            IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                && DateTime.Compare(pg.DateCreated, eday) < 0);
            List<string> country = new List<string>();


            foreach (var pg in pgs)
            {
                country.Add(pg.Country);
            }

            IEnumerable<string> distinctCountry = country.Distinct();
            country = distinctCountry.ToList();
            return distinctCountry.ToArray();
        }

        private int[,] getCountryTable(string[] disCountry, DateTime sday, DateTime eday)
        {
            //row means language, column means center
            int[,] countryTable = new int[100, 10];

            IEnumerable<PrimaryGuardian> aa = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                        && DateTime.Compare(pg.DateCreated, eday) < 0);

            foreach (PrimaryGuardian pg in aa)
            {
                int cId = pg.CenterId;
                string country = pg.Country;
                int countryIndex = disCountry.ToList<string>().IndexOf(country);

                countryTable[countryIndex, cId] += 1;
            }

            //get row total
            for (int i = 0; i < disCountry.Length; i++)
            {
                int rowTotal = 0;
                for (int j = 1; j < context.Centers.Count() + 1; j++)
                {
                    rowTotal += countryTable[i, j];
                }
                int length = context.Centers.Count() + 1;
                countryTable[i, length] = rowTotal;
            }

            return countryTable;
        }


        private int[,] getLanguageTable(string[] disLanguage, DateTime sday, DateTime eday)
        {
            //row means language, column means center
            int[,] languageTable = new int[100,10];

            IEnumerable<PrimaryGuardian> aa = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                        && DateTime.Compare(pg.DateCreated, eday) < 0);
            
            foreach (PrimaryGuardian pg in aa)
            {
                int cId = pg.CenterId;
                string language = pg.Language;
                int languageIndex = disLanguage.ToList<string>().IndexOf(language);

                languageTable[languageIndex, cId] += 1;
            }

            for (int i = 0; i < disLanguage.Length; i++)
            {
                int rowTotal = 0;
                for (int j = 1; j < context.Centers.Count()+1; j++)
                {
                    rowTotal += languageTable[i, j];
                }
                int length = context.Centers.Count()+1;
                languageTable[i, length] = rowTotal;
            }

            return languageTable;
        }


        


        
    }
}

