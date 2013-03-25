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

            // fill in the 2-dimensional string array here with data
            var Content = new string[][]
            {
                new string[] {"First Name", "Last Name", "Postal Code", "City"},
                new string[] {},
                new string[] {"Peter", "Moon", "1234", "Canada"},
                new string[] {"Rufus", "Zhu", "5678", "Canada"}
            };


            // create a new excel workbook
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = workBook.ActiveSheet;

            // File in the excel cells with report data here if we are in an excel mode
            if (mode == 1 || mode == 2)
            {
                for (int i = 0; i < Content.Length; i++)
                {
                    string[] CsvLine = Content[i];
                    for (int j = 0; j < CsvLine.Length; j++)
                    {
                        sheet.Cells[i + 1, j + 1] = CsvLine[j];
                    }
                }
            }

            // filename for temporary excel file
            string tempFileName = Path.GetTempFileName();
            System.IO.File.Delete(tempFileName); // delete the file created so we can save as

            if (mode == 1)
            {
                // .xls file format, 2003 and older
                workBook.SaveAs(tempFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlAddIn8);
                workBook.Close();

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment; filename=Report.xls");

                Response.WriteFile(tempFileName);
                Response.Flush();

                // clean temp file
                System.IO.File.Delete(tempFileName);
                Response.End();
            }
            else if (mode == 2)
            {
                // .xlsx file format, 2007 and newer
                // maybe try to find the corresponding XlFileFormat so we don't rely on the Interop version to choose
                workBook.SaveAs(tempFileName);
                workBook.Close();

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");

                Response.WriteFile(tempFileName);
                Response.Flush();

                // clean temp file
                System.IO.File.Delete(tempFileName);
                Response.End();
            }
            else if (mode == 3)
            {

                // csv version 2, uses the same 2-dimensional string array from the excel modes
                Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
                Response.ContentType = "text/plain; charset=UTF-8";

                for (int i = 0; i < Content.Length; i++)
                {
                    string[] CsvLine = Content[i];

                    if (CsvLine.Length == 0)
                    {
                        continue;
                    }

                    // first one doesn't have comma
                    byte[] val1 = new System.Text.UTF8Encoding(true).GetBytes(CsvLine[0]);
                    Response.OutputStream.Write(val1, 0, val1.Length);

                    // insert commas before the rest of the values
                    for (int j = 1; j < CsvLine.Length; j++)
                    {
                        byte[] byteArray = new System.Text.UTF8Encoding(true).GetBytes("," + CsvLine[j]);
                        Response.OutputStream.Write(byteArray, 0, byteArray.Length);
                    }

                    // newline
                    byte[] newLine = new System.Text.UTF8Encoding(true).GetBytes("\r\n");
                    Response.OutputStream.Write(newLine, 0, newLine.Length);
                }

                Response.End();
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

        private int[] getNumOfChild(DateTime eday)
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

            return languageTable;
        }





        
    }
}

