﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OFRPDMS.Models;
using OFRPDMS.Areas.Admin.Models;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using OFRPDMS.Account;
using OFRPDMS.Repositories;
using Ninject;
using System.Reflection;

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        //OFRPDMSContext context = new OFRPDMSContext();
        private IRepositoryService repoService;
        private IAccountService account;


        public ReportController() { }

        [Inject]
        public ReportController(IAccountService account, IRepositoryService repoService)
        {

            this.account = account;
            this.repoService = repoService;
        }
        //
        // GET: /Report/

        public ActionResult Index()
        {
            
            return View();
        }

        //
        // GET: /Report/Generate

        [HttpPost]
        public ActionResult Generate(Report report, int mode)
        {
            
            DateTime startday = report.startDay;
            DateTime endday = report.endDay.AddDays(1);
            string[,] pgTable = getStringPGTable(report.startDay, endday);
            string[,] languageTable = getStringLanguageTable(report.startDay, endday);
            string[,] countryTable = getStringCountryTable(report.startDay, endday);
            ViewBag.myReport = report;
            ViewBag.center = repoService.centerRepo.FindAll().ToArray();
            ViewBag.pgTable = pgTable;
            ViewBag.languageTable = languageTable;
            ViewBag.countryTable = countryTable;

            if (mode == 1)
            {
                return View();
            }
            else if (mode == 2)
            {
                
                Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
                Response.ContentType = "text/plain; charset=UTF-8";
                parseStringTable(pgTable);
                parseStringTable(countryTable);
                parseStringTable(languageTable);

                Response.End();

                
            }
            else if (mode == 3)
            {
                // filename for temporary excel file
                string tempFileName = Path.GetTempFileName();
                System.IO.File.Delete(tempFileName); // delete the file created so we can save as
                tempFileName += ".xlsx";

                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(tempFileName, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                sheets.Append(sheet);

                // Get the sheetData cell table.
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();



                uint rowIndex = 1;

                for (uint i = 0; i < pgTable.GetLength(0); i++)
                {
                    // add a row to the cell table
                    Row row;
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);

                    // populate the row
                    for (uint j = 0; j < pgTable.GetLength(1); j++)
                    {
                        CreateCell(row, pgTable[i, j], j);
                    }
                    rowIndex++;
                }

                // one blank row to separate data
                rowIndex++;

                for (uint i = 0; i < countryTable.GetLength(0); i++)
                {
                    // add a row to the cell table
                    Row row;
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);

                    // populate the row
                    for (uint j = 0; j < countryTable.GetLength(1); j++)
                    {
                        CreateCell(row, countryTable[i, j], j);
                    }
                    rowIndex++;
                }

                // one blank row to separate data
                rowIndex++;

                for (uint i = 0; i < languageTable.GetLength(0); i++)
                {
                    // add a row to the cell table
                    Row row;
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);

                    // populate the row
                    for (uint j = 0; j < languageTable.GetLength(1); j++)
                    {
                        CreateCell(row, languageTable[i, j], j);
                    }
                    rowIndex++;
                }

                workbookpart.Workbook.Save();
                spreadsheetDocument.Close();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");

                Response.WriteFile(tempFileName);
                Response.Flush();

                // clean temp file
                System.IO.File.Delete(tempFileName);
                Response.End();
            }
            return View();
            
        }

        //
        // GET: /Report/Generate

        [HttpPost]
        public ActionResult TrackPG(Report report)
        {
            if (report.type == "Primary")
            {
                ViewBag.first = repoService.primaryGuardianRepo.FindById(report.pgid).FirstName;
                ViewBag.last = repoService.primaryGuardianRepo.FindById(report.pgid).LastName;
                ViewBag.pgid = report.pgid;
            }
            else if (report.type == "Child")
            {
                ViewBag.first = repoService.childRepo.FindById(report.pgid).FirstName;
                ViewBag.last = repoService.childRepo.FindById(report.pgid).LastName;
            }

            ViewBag.type = report.type;
  
            var repo = getVisitHistory(report.startDay2, report.endDay2, report.type, report.pgid);
            return View(repo);
        }

        //
        // Post: /Index

        [HttpPost]
        public ActionResult Index(Report report)
        {

            DateTime startDay = report.startDay;
            DateTime endDay = report.endDay;


            ViewBag.myReport = report;
            ViewBag.numOfNewPG = getNumOfNewPGTable(startDay, endDay);


            return RedirectToAction("Generate","Report");
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

            // newline after this table
            byte[] newLine2 = new System.Text.UTF8Encoding(true).GetBytes("\r\n");
            Response.OutputStream.Write(newLine2, 0, newLine2.Length);
        }





        [HttpPost]
        public ActionResult Search(string name, string type)
        {
            if (type == "Primary")
            {
                var _primaryguardian = repoService.primaryGuardianRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Country", "Email", "Language", "Phone", "PostalCodePrefix", "Allergies", "DateCreated" };
                IEnumerable<PropertyInfo> properties = typeof(PrimaryGuardian).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper()))));
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
                var _primaryguardian = repoService.childRepo.FindAll();
                string[] searchFields = new string[] { "FirstName", "LastName", "Allergies" };
                IEnumerable<PropertyInfo> properties = typeof(Child).GetProperties().Where(prop => searchFields.Contains(prop.Name));
                _primaryguardian = _primaryguardian.Where(
                      p => (properties.Any(prop => prop.GetValue(p, null) != null && prop.GetValue(p, null).ToString().ToUpper().Contains(name.ToUpper())))); 
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
            int[] newPGTable = new int[repoService.centerRepo.FindAll().Count()];
            int counter = 0;
            foreach (var c in repoService.centerRepo.FindAll())
            {
                IEnumerable<PrimaryGuardian> pgs = repoService.primaryGuardianRepo.FindAllWithCenterId(c.Id).
                    Where(pg => DateTime.Compare(pg.DateCreated, sday) 
                        > 0 && DateTime.Compare(pg.DateCreated, eday) < 0);
                newPGTable[counter] = pgs.Count();
                counter++;
            }

            return newPGTable;
        }
        private int[] getNumOfEventParticipantTable(DateTime sday, DateTime eday)
        {
            int[] newVisitTable = new int[repoService.centerRepo.FindAll().Count()];
            int counter = 0;

            foreach (var c in repoService.centerRepo.FindAll())
            {
                int sumEventParticipants = 0;
                List<Event> evts = repoService.eventRepo.FindAllWithCenterId(c.Id).Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) <= 0).ToList();
                for (int i = 0; i < evts.Count; i++ )
                {
                    int epcount = evts[i].EventParticipants.Count;
                    sumEventParticipants += epcount;
                }
                
                newVisitTable[counter] = sumEventParticipants;
                counter++;
            }

            return newVisitTable;
        }


        private int[] getNumOfVisitTable(DateTime sday, DateTime eday)
        {
            int[] newVisitTable = new int[repoService.centerRepo.FindAll().Count()];
            int counter = 0;
            foreach (var c in repoService.centerRepo.FindAll())
            {
                IEnumerable<Event> evts = repoService.eventRepo.FindAllWithCenterId(c.Id).Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) <= 0);
                newVisitTable[counter] = evts.Count();
                counter++;
            }

            return newVisitTable;
        }


        private List<EventParticipant> getVisitHistory(DateTime sday, DateTime eday, string type, int id)
        {
            List<EventParticipant> dt = new List<EventParticipant>();
            if (type == "Primary")
            {
                PrimaryGuardian pg = repoService.primaryGuardianRepo.FindById(id);
                IEnumerable<EventParticipant> evtps = repoService.signInRepo.FindAll()
                    .Where(evtp => evtp.PrimaryGuardianId == id
                    && DateTime.Compare(evtp.Event.Date, sday) > 0
                    && DateTime.Compare(evtp.Event.Date, eday) <= 0);
                evtps.OrderBy(evtp => evtp.Event.Date);
                foreach(var evtp in evtps)
                {
                    dt.Add(evtp);
                }              
            }
            else if (type == "Child")
            {
                Child pg = repoService.childRepo.FindById(id);
                IEnumerable<EventParticipant> evtps = 
                    repoService.signInRepo.FindAll().Where(evtp => evtp.ChildId == id
                    && DateTime.Compare(evtp.Event.Date, sday) > 0
                    && DateTime.Compare(evtp.Event.Date, eday) <= 0);
                evtps.OrderBy(evtp => evtp.Event.Date);
                foreach (var evtp in evtps)
                {
                    dt.Add(evtp);
                }
            }
            return dt;
        }
   
        
        private int[] getNumOfChild(DateTime eday)
        {
            int[] chTable = new int[repoService.centerRepo.FindAll().Count()];

            int counter = 0;
            foreach (var c in repoService.centerRepo.FindAll())
            {
                IEnumerable<Child> chs = repoService.childRepo.FindAll().Where
                    (ch => DateTime.Compare(ch.DateCreated, eday) < 0 
                        && ch.PrimaryGuardian.CenterId == c.Id);
                chTable[counter] = chs.Count();
                counter++;
            }

            return chTable;
        }

        private int[] getNumOfNewChild(DateTime sday, DateTime eday)
        {
            int[] chTable = new int[repoService.centerRepo.FindAll().Count()];

            int counter = 0;
            foreach (var c in repoService.centerRepo.FindAll())
            {
                IEnumerable<Child> chs = repoService.childRepo.FindAll().Where(ch => DateTime.Compare(ch.DateCreated, sday) >= 0 
                    && DateTime.Compare(ch.DateCreated, eday) <= 0 
                    && ch.PrimaryGuardian.CenterId == c.Id);
                chTable[counter] = chs.Count();
                counter++;
            }

            return chTable;
        }

        private string[,] getStringPGTable(DateTime startDay, DateTime endDay)
        {

            Center[] center = repoService.centerRepo.FindAll().ToArray();
            string[,] pgString = new string[center.Length+1,7];
            string[] disCountry = getCountrys(startDay, endDay);
            int[] numOfNewPG = getNumOfNewPGTable(startDay, endDay);
            int[] numOfPG = getNumOfPGTable(endDay);
            int[] numOfVisit = getNumOfEventParticipantTable(startDay, endDay);
            int[] numOfSession = getNumOfVisitTable(startDay, endDay);
            int[] numOfChild = getNumOfChild(endDay);
            int[] numOfNewChild = getNumOfNewChild(startDay, endDay);
            //----------------------------------------------------
            //fill in # of parents table
            //first row
            pgString[0, 0] = "Center";
            pgString[0, 1] = "# of new parents";
            pgString[0, 2] = "# of parents";
            pgString[0, 3] = "# of sessions";
            pgString[0, 4] = "# of visits";
            pgString[0, 5] = "# of new child";
            pgString[0, 6] = "# of child";

            //start from second row
            int i = 0;
            foreach (Center c in repoService.centerRepo.FindAll())
            {
                pgString[i+1, 0] = c.Name;
                pgString[i+1, 1] = numOfNewPG[i].ToString();
                pgString[i+1, 2] = numOfPG[i].ToString();
                pgString[i+1, 3] = numOfSession[i].ToString();
                pgString[i+1, 4] = numOfVisit[i].ToString();
                pgString[i+1, 5] = numOfNewChild[i].ToString();
                pgString[i+1, 6] = numOfChild[i].ToString();
                i++;
            }
            return pgString;
        }

        private string[,] getStringLanguageTable(DateTime startDay,DateTime endDay)
        {
            

            string[] disLanguage = getLanguages(startDay, endDay);
            int[,] languageTable = getLanguageTable(disLanguage, startDay, endDay);
            string[,] languageString = new string[disLanguage.Length + 1, repoService.centerRepo.FindAll().Count() + 2];
            int cLength = repoService.centerRepo.FindAll().Count();

            //first row
            languageString[0, 0] = "Language";
            int counter = 1;
            foreach (Center c in repoService.centerRepo.FindAll())
            {
                languageString[0, counter] = c.Name;
                counter++;
            }
            languageString[0, cLength + 1] = "Total";

            for (int i = 0; i < disLanguage.Length; i++)
            {
                languageString[i+1, 0] = disLanguage[i];
                for (int j = 0; j < cLength + 1; j++)
                {
                     languageString[i+1, j+1] = languageTable[i,j].ToString();
                }
            }
            return languageString;
        }

        private string[,] getStringCountryTable(DateTime startDay,DateTime endDay)
        {

            int cLength = repoService.centerRepo.FindAll().Count();
            string[] disCountry = getCountrys(startDay, endDay);
            int[,] countryTable = getCountryTable(disCountry, startDay, endDay);
            string[,] countryString = new string[disCountry.Length + 1, cLength+2];

            //first row
            countryString[0, 0] = "Country";
            int counter = 1;
            foreach (Center c in repoService.centerRepo.FindAll())
            {
                countryString[0, counter] = c.Name;
                counter++;
            }
            countryString[0, repoService.centerRepo.FindAll().Count() + 1] = "Total";

            //the rest
            for (int i = 0; i < disCountry.Length; i++)
            {
                countryString[i+1, 0] = disCountry[i];
                for(int j = 0; j < cLength+1; j++)
                {
                     countryString[i+1, j+1] = countryTable[i,j].ToString();
                }
            }
            return countryString;
        }

        private int[] getNumOfPGTable(DateTime eday)
        {
            int[] pgTable = new int[repoService.centerRepo.FindAll().Count() + 1];
            int counter = 0;
            foreach (var c in repoService.centerRepo.FindAll())
            {
                IEnumerable<PrimaryGuardian> pgs = repoService.primaryGuardianRepo.FindAllWithCenterId(c.Id)
                    .Where(pg => DateTime.Compare(pg.DateCreated, eday) < 0);
                pgTable[counter] = pgs.Count();
                counter++;
            }

            return pgTable;
        }


        private string[] getLanguages(DateTime sday, DateTime eday)
        {
            IEnumerable<PrimaryGuardian> pgs = repoService.primaryGuardianRepo.FindAll().Where
                (pg => DateTime.Compare(pg.DateCreated, sday) > 0
                && DateTime.Compare(pg.DateCreated, eday) < 0 );
            List<string> language = new List<string>();
            
            
            foreach (var pg in pgs)
            {
                if(pg.Language != null)
                language.Add(pg.Language);
            }

            IEnumerable<string> distinctLanguage = language.Distinct();
            language = distinctLanguage.ToList();
            return distinctLanguage.ToArray();
        }

        private string[] getCountrys(DateTime sday, DateTime eday)
        {
            IEnumerable<PrimaryGuardian> pgs = repoService.primaryGuardianRepo.FindAll()
                .Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                && DateTime.Compare(pg.DateCreated, eday) < 0);
            List<string> country = new List<string>();


            foreach (var pg in pgs)
            {
                if(pg.Country !=null)
                country.Add(pg.Country);
            }

            IEnumerable<string> distinctCountry = country.Distinct();
            country = distinctCountry.ToList();
            return distinctCountry.ToArray();
        }

        private int[,] getCountryTable(string[] disCountry, DateTime sday, DateTime eday)
        {
            //row means language, column means center
            int[,] countryTable = new int[disCountry.Length, repoService.centerRepo.FindAll().Count()+ 1];

            int counter = 0;
            foreach (Center c in repoService.centerRepo.FindAll())
            {
                IEnumerable<PrimaryGuardian> aa = repoService.primaryGuardianRepo.FindAllWithCenterId(c.Id).
                       Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                       && DateTime.Compare(pg.DateCreated, eday) < 0
                       && pg.CenterId == c.Id);
                for (int i = 0; i < disCountry.Length; i++)
                {
                    countryTable[i, counter] = aa.Where(pg => pg.Country == disCountry[i]).Count();
                }
                counter++;
            }

            //get row total
            for (int i = 0; i < disCountry.Length; i++)
            {
                int rowTotal = 0;
                for (int j = 0; j < repoService.centerRepo.FindAll().Count(); j++)
                {
                    rowTotal += countryTable[i, j];
                }
                int length = repoService.centerRepo.FindAll().Count();
                countryTable[i, length] = rowTotal;
            }

            return countryTable;
        }


        private int[,] getLanguageTable(string[] disLanguage, DateTime sday, DateTime eday)
        {
            //row means language, column means center
            int[,] languageTable = new int[disLanguage.Length, repoService.centerRepo.FindAll().Count() + 1];


            int counter = 0;
            foreach (Center c in repoService.centerRepo.FindAll())
            {
                IEnumerable<PrimaryGuardian> aa = repoService.primaryGuardianRepo.FindAllWithCenterId(c.Id)
                    .Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                       && DateTime.Compare(pg.DateCreated, eday) < 0);
                for(int i = 0 ; i < disLanguage.Length; i++)
                {
                    languageTable[i, counter] = aa.Where(pg => pg.Language == disLanguage[i]).Count();
                }
                counter++;
            }


            for (int i = 0; i < disLanguage.Length; i++)
            {
                int rowTotal = 0;
                for (int j = 0; j < repoService.centerRepo.FindAll().Count(); j++)
                {
                    rowTotal += languageTable[i, j];
                }
                int length = repoService.centerRepo.FindAll().Count();
                languageTable[i, length] = rowTotal;
            }

            return languageTable;
        }


        public static void CreateCell(Row row, string text, uint columnIndex)
        {
            Cell newCell = new Cell();

            newCell.CellValue = new CellValue(text);

            try
            {
                int value = Convert.ToInt32(text);
                newCell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }
            catch (FormatException e)
            {
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);
            }

            row.InsertAt(newCell, (int)columnIndex);
        }
        
    }
}

