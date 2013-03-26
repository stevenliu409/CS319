using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using OFRPDMS.Areas.Admin.Models;

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        OFRPDMSContext context = new OFRPDMSContext();

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
            DateTime startday = report.startDay;
            DateTime endday = report.endDay.AddDays(1);
            string[] disLanguage = getLanguages(report.startDay, endday);
            string[] disCountry = getCountrys(report.startDay, endday);
            ViewBag.myReport = report;
            ViewBag.numOfNewPG = getNumOfNewPGTable(report.startDay, endday);
            ViewBag.numOfPG = getNumOfPGTable(endday);
            ViewBag.numOfVisit = getNumOfVisitTable(report.startDay, endday);
            ViewBag.distinctLanguage = disLanguage;
            ViewBag.distinctCountry = disCountry;
            ViewBag.center = context.Centers.ToArray();
            ViewBag.languageTable = getLanguageTable(disLanguage, report.startDay, endday);
            ViewBag.countryTable = getCountryTable(disCountry, report.startDay, endday);
            return View(context.Centers);
            
        }

        //
        // GET: /Report/Generate

        [HttpPost]
        public ActionResult TrackPG(Report report)
        {
            if (report.type == "Primary")
            {
                ViewBag.first = context.PrimaryGuardians.Find(report.pgid).FirstName;
                ViewBag.last = context.PrimaryGuardians.Find(report.pgid).LastName;
                ViewBag.pgid = report.pgid;
            }
            else if (report.type == "Child")
            {
                ViewBag.first = context.Children.Find(report.pgid).FirstName;
                ViewBag.last = context.Children.Find(report.pgid).LastName;
            }

            ViewBag.type = report.type;
  
            ViewBag.visitHistory = getVisitHistory(report.startDay2, report.endDay2, report.type, report.pgid).ToArray();
            
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


        public void GenerateExcel(Report report, int mode)
        {
            Response.Clear();
            DateTime startDay = report.startDay3;
            DateTime endDay = report.endDay3.AddDays(1);
            // fill in the 2-dimensional string array here with data
            string[,] Content1 = getStringPGTable(startDay,endDay);
            string[,] Content2 = getStringLanguageTable(startDay, endDay);
            string[,] Content3 = getStringCountryTable(startDay, endDay);

            // create a new excel workbook
            //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook workBook = excel.Workbooks.Add();
            //Microsoft.Office.Interop.Excel.Worksheet sheet = workBook.ActiveSheet;

            //File in the excel cells with report data here if we are in an excel mode
            //if (mode == 1 || mode == 2)
            //{
            //    //adding table 1
            //    for (int i = 0; i < Content1.GetLength(0); i++)
            //    {

            //        for (int j = 0; j < Content1.GetLength(1); j++)
            //        {
            //            sheet.Cells[i, j] = Content1[i,j];
            //        }
            //    }
            //    //adding table 2
            //    for (int i = Content1.GetLength(0); i < Content1.GetLength(0) + Content2.GetLength(0); i++)
            //    {

            //        for (int j = Content1.GetLength(1); j < Content1.GetLength(1) + Content2.GetLength(1); j++)
            //        {
            //            sheet.Cells[i, j] = Content2[i, j];
            //        }
            //    }
            //    //adding table 3
            //    for (int i = Content1.GetLength(0) + Content2.GetLength(0); i < Content1.GetLength(0) + Content2.GetLength(0) + Content3.GetLength(0); i++)
            //    {

            //        for (int j = Content1.GetLength(1) + Content2.GetLength(1); j < Content1.GetLength(1) + Content2.GetLength(1) + Content3.GetLength(1); j++)
            //        {
            //            sheet.Cells[i, j] = Content1[i, j];
            //        }
            //    }
            //}

            // filename for temporary excel file
            string tempFileName = Path.GetTempFileName();
            System.IO.File.Delete(tempFileName); // delete the file created so we can save as

            if (mode == 1)
            {
                //// .xls file format, 2003 and older
                //workBook.SaveAs(tempFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlAddIn8);
                //workBook.Close();

                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("content-disposition", "attachment; filename=Report.xls");

                //Response.WriteFile(tempFileName);
                //Response.Flush();

                //// clean temp file
                //System.IO.File.Delete(tempFileName);
                //Response.End();
            }
            else if (mode == 2)
            {
                //// .xlsx file format, 2007 and newer
                //// maybe try to find the corresponding XlFileFormat so we don't rely on the Interop version to choose
                //workBook.SaveAs(tempFileName);
                //workBook.Close();

                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");

                //Response.WriteFile(tempFileName);
                //Response.Flush();

                //// clean temp file
                //System.IO.File.Delete(tempFileName);
                //Response.End();
            }
            else if (mode == 3)
            {

                // csv version 2, uses the same 2-dimensional string array from the excel modes
                Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
                Response.ContentType = "text/plain; charset=UTF-8";

                parseStringTable(getStringPGTable(startDay, endDay));
                parseStringTable(getStringLanguageTable(startDay, endDay));
                parseStringTable(getStringCountryTable(startDay, endDay));
                
                
                
             }

                Response.End();
            
        }

        private void parseStringTable(string[,] Content)
        {
            for (int i = 0; i < Content.GetLength(0); i++)
            {

                byte[] val1 = new System.Text.UTF8Encoding(true).GetBytes(Content[i, 0]);
                Response.OutputStream.Write(val1, 0, val1.Length);

                for (int j = 1; j < Content.GetLength(1); j++)
                {
                    // first one doesn't have comma

                    // insert commas before the rest of the values

                    byte[] byteArray = new System.Text.UTF8Encoding(true).GetBytes("," + Content[i, j]);
                    Response.OutputStream.Write(byteArray, 0, byteArray.Length);
                }
                // newline
                byte[] newLine = new System.Text.UTF8Encoding(true).GetBytes("\r\n");
                Response.OutputStream.Write(newLine, 0, newLine.Length);
            }
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
            else
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

            
        }

        // get the number of new PG(created after start date) 
        private int[] getNumOfNewPGTable(DateTime sday, DateTime eday)
        {
            int[] newPGTable = new int[context.Centers.Count()+10];
            int test = newPGTable.Length;
            foreach (var c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0 && DateTime.Compare(pg.DateCreated, eday) < 0 && pg.CenterId == c.Id);
                newPGTable[c.Id] = pgs.Count();
            }

            return newPGTable;
        }
        private int[] getNumOfVisitTable(DateTime sday, DateTime eday)
        {
            int[] newVisitTable = new int[context.Centers.Count()+10];

            foreach (var c in context.Centers)
            {
                IEnumerable<Event> evts = context.Events.Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) < 0 && evt.CenterId == c.Id);
                newVisitTable[c.Id] = evts.Count();
            }

            return newVisitTable;
        }

        private List<DateTime> getVisitHistory(DateTime sday, DateTime eday, string type, int id)
        {
            List<DateTime> dt = new List<DateTime>();
            if (type == "Primary")
            {
                PrimaryGuardian pg = context.PrimaryGuardians.Find(id);
                IEnumerable<EventParticipant> evtps = context.EventParticipants.Where(evtp => evtp.PrimaryGuardianId == id
                    && DateTime.Compare(evtp.Event.Date, sday) > 0
                    && DateTime.Compare(evtp.Event.Date, eday) < 0);
                evtps.OrderBy(evtp => evtp.Event.Date);
                foreach(var evtp in evtps)
                {
                    dt.Add(evtp.Event.Date);
                }              
            }
            else if (type == "Child")
            {
                Child pg = context.Children.Find(id);
                IEnumerable<EventParticipant> evtps = context.EventParticipants.Where(evtp => evtp.ChildId == id
                    && DateTime.Compare(evtp.Event.Date, sday) > 0
                    && DateTime.Compare(evtp.Event.Date, eday) < 0);
                evtps.OrderBy(evtp => evtp.Event.Date);
                foreach (var evtp in evtps)
                {
                    dt.Add(evtp.Event.Date);
                }
            }
            return dt;
        }
   
        //not implemented because the Child doesn't have a DateCreated
        //private int[] getNumOfChild(DateTime eday)
        //{
        //    int[] pgTable = new int[context.Centers.Count()];
            
        //    foreach (var c in context.Centers)
        //    {
        //        IEnumerable<PrimaryGuardian> chs = context.Children.Where(ch => DateTime.Compare(ch., eday) < 0 && pg.CenterId == c.Id);
        //        IEnumerable<PrimaryGuardian> distinctPG = context.PrimaryGuardians.Distinct();
        //        pgTable[c.Id] = pgs.Count();
        //    }

        //    return pgTable;
        //}

        private string[,] getStringPGTable(DateTime startDay, DateTime endDay)
        {

            Center[] center = context.Centers.ToArray();
            string[,] pgString = new string[center.Length+1,4];
            string[] disCountry = getCountrys(startDay, endDay);
            int[] numOfNewPG = getNumOfNewPGTable(startDay, endDay);
            int[] numOfPG = getNumOfPGTable(endDay);
            int[] numOfVisit = getNumOfVisitTable(startDay, endDay);
            
            //----------------------------------------------------
            //fill in # of parents table
            //first row
            pgString[0, 0] = "Center";
            pgString[0, 1] = "# of new parents";
            pgString[0, 2] = "# of parents";
            pgString[0, 3] = "# of visit";

            //start from second row
            for (int i = 1; i < context.Centers.Count() + 1; i++)
            {
                pgString[i, 0] = context.Centers.Find(i).Name;
                pgString[i, 1] = numOfNewPG[i].ToString();
                pgString[i, 2] = numOfPG[i].ToString();
                pgString[i, 3] = numOfVisit[i].ToString();
            }
            return pgString;
        }

        private string[,] getStringLanguageTable(DateTime startDay,DateTime endDay)
        {
            Center[] center = context.Centers.ToArray();

            string[] disLanguage = getLanguages(startDay, endDay);
            int[,] languageTable = getLanguageTable(disLanguage, startDay, endDay);
            string[,] languageString = new string[disLanguage.Length + 1, center.Length+2];

            //first row
            languageString[0, 0] = "Language";
            for (int i = 1; i < context.Centers.Count() + 1; i++)
            {
                languageString[0, i] = context.Centers.Find(i).Name;
            }
            languageString[0, context.Centers.Count() + 1] = "Total";

            for (int i = 0; i < disLanguage.Length; i++)
            {
                languageString[i+1, 0] = disLanguage[i];
                for(int j = 1; j < center.Length+2; j++)
                {
                     languageString[i+1, j] = languageTable[i,j].ToString();
                }
            }
            return languageString;
        }

        private string[,] getStringCountryTable(DateTime startDay,DateTime endDay)
        {
            Center[] center = context.Centers.ToArray();

            string[] disCountry = getCountrys(startDay, endDay);
            int[,] languageTable = getCountryTable(disCountry, startDay, endDay);
            string[,] countryString = new string[disCountry.Length + 1, center.Length+2];

            //first row
            countryString[0, 0] = "Country";
            for (int i = 1; i < context.Centers.Count() + 1; i++)
            {
                countryString[0, i] = context.Centers.Find(i).Name;
            }
            countryString[0, context.Centers.Count() + 1] = "Total";

            for (int i = 0; i < disCountry.Length; i++)
            {
                countryString[i+1, 0] = disCountry[i];
                for(int j = 1; j < center.Length+2; j++)
                {
                     countryString[i+1, j] = languageTable[i,j].ToString();
                }
            }
            return countryString;
        }

        private int[] getNumOfPGTable(DateTime eday)
        {
            int[] pgTable = new int[context.Centers.Count() + 10];

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
            int[,] countryTable = new int[disCountry.Length+1, context.Centers.Count()+20];

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
            int[,] languageTable = new int[disLanguage.Length+1, context.Centers.Count()+10];

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

