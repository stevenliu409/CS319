using System;
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

namespace OFRPDMS.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        OFRPDMSContext context = new OFRPDMSContext();
      

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
            ViewBag.center = context.Centers.ToArray();
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

            DateTime startDay = report.startDay;
            DateTime endDay = report.endDay;


            ViewBag.myReport = report;
            ViewBag.numOfNewPG = getNumOfNewPGTable(startDay, endDay);


            return RedirectToAction("Generate","Report");
        }


        //public void GenerateExcel(Report report, int mode)
        //{
        //    Response.Clear();
        //    DateTime startDay = report.startDay3;
        //    DateTime endDay = report.endDay3.AddDays(1);
        //    // fill in the 2-dimensional string array here with data
        //    //string[,] Content1 = getStringPGTable(startDay,endDay);
        //    //string[,] Content2 = getStringLanguageTable(startDay, endDay);
        //    //string[,] Content3 = getStringCountryTable(startDay, endDay);

        //    // create a new excel workbook
        //    //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        //    //Microsoft.Office.Interop.Excel.Workbook workBook = excel.Workbooks.Add();
        //    //Microsoft.Office.Interop.Excel.Worksheet sheet = workBook.ActiveSheet;

        //    //File in the excel cells with report data here if we are in an excel mode
        //    //if (mode == 1 || mode == 2)
        //    //{
        //    //    //adding table 1
        //    //    for (int i = 0; i < Content1.GetLength(0); i++)
        //    //    {

        //    //        for (int j = 0; j < Content1.GetLength(1); j++)
        //    //        {
        //    //            sheet.Cells[i, j] = Content1[i,j];
        //    //        }
        //    //    }
        //    //    //adding table 2
        //    //    for (int i = Content1.GetLength(0); i < Content1.GetLength(0) + Content2.GetLength(0); i++)
        //    //    {

        //    //        for (int j = Content1.GetLength(1); j < Content1.GetLength(1) + Content2.GetLength(1); j++)
        //    //        {
        //    //            sheet.Cells[i, j] = Content2[i, j];
        //    //        }
        //    //    }
        //    //    //adding table 3
        //    //    for (int i = Content1.GetLength(0) + Content2.GetLength(0); i < Content1.GetLength(0) + Content2.GetLength(0) + Content3.GetLength(0); i++)
        //    //    {

        //    //        for (int j = Content1.GetLength(1) + Content2.GetLength(1); j < Content1.GetLength(1) + Content2.GetLength(1) + Content3.GetLength(1); j++)
        //    //        {
        //    //            sheet.Cells[i, j] = Content1[i, j];
        //    //        }
        //    //    }
        //    //}

        //    // filename for temporary excel file
        //    string tempFileName = Path.GetTempFileName();
        //    System.IO.File.Delete(tempFileName); // delete the file created so we can save as

        //    if (mode == 1)
        //    {
        //        //// .xls file format, 2003 and older
        //        //workBook.SaveAs(tempFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlAddIn8);
        //        //workBook.Close();

        //        //Response.ContentType = "application/vnd.ms-excel";
        //        //Response.AddHeader("content-disposition", "attachment; filename=Report.xls");

        //        //Response.WriteFile(tempFileName);
        //        //Response.Flush();

        //        //// clean temp file
        //        //System.IO.File.Delete(tempFileName);
        //        //Response.End();
        //    }
        //    else if (mode == 2)
        //    {
        //        //// .xlsx file format, 2007 and newer
        //        //// maybe try to find the corresponding XlFileFormat so we don't rely on the Interop version to choose
        //        //workBook.SaveAs(tempFileName);
        //        //workBook.Close();

        //        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        //Response.AddHeader("content-disposition", "attachment; filename=Report.xlsx");

        //        //Response.WriteFile(tempFileName);
        //        //Response.Flush();

        //        //// clean temp file
        //        //System.IO.File.Delete(tempFileName);
        //        //Response.End();
        //    }
        //    else if (mode == 3)
        //    {

        //        // csv version 2, uses the same 2-dimensional string array from the excel modes
        //        Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
        //        Response.ContentType = "text/plain; charset=UTF-8";

        //        parseStringTable(getStringPGTable(startDay, endDay));
        //        parseStringTable(getStringLanguageTable(startDay, endDay));
        //        parseStringTable(getStringCountryTable(startDay, endDay));
                
                
                
        //     }

        //        Response.End();
            
        //}

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
            int[] newPGTable = new int[context.Centers.Count()];
            int counter = 0;
            foreach (var c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0 && DateTime.Compare(pg.DateCreated, eday) < 0 && pg.CenterId == c.Id);
                newPGTable[counter] = pgs.Count();
                counter++;
            }

            return newPGTable;
        }
        private int[] getNumOfEventParticipantTable(DateTime sday, DateTime eday)
        {
            int[] newVisitTable = new int[context.Centers.Count()];
            int counter = 0;
            
            foreach (var c in context.Centers)
            {
                int sumEventParticipants = 0;
                List<Event> evts = context.Events.Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) <= 0 && evt.CenterId == c.Id).ToList();
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
            int[] newVisitTable = new int[context.Centers.Count()];
            int counter = 0;
            foreach (var c in context.Centers)
            {
                IEnumerable<Event> evts = context.Events.Where(evt => DateTime.Compare(evt.Date, sday) > 0 && DateTime.Compare(evt.Date, eday) < 0 && evt.CenterId == c.Id);
                newVisitTable[counter] = evts.Count();
                counter++;
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
                    && DateTime.Compare(evtp.Event.Date, eday) <= 0);
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
                    && DateTime.Compare(evtp.Event.Date, eday) <= 0);
                evtps.OrderBy(evtp => evtp.Event.Date);
                foreach (var evtp in evtps)
                {
                    dt.Add(evtp.Event.Date);
                }
            }
            return dt;
        }
   
        
        private int[] getNumOfChild(DateTime eday)
        {
            int[] chTable = new int[context.Centers.Count()];

            int counter = 0;
            foreach (var c in context.Centers)
            {
                IEnumerable<Child> chs = context.Children.Where(ch => DateTime.Compare(ch.DateCreated, eday) < 0 && ch.PrimaryGuardian.CenterId == c.Id);
                chTable[counter] = chs.Count();
                counter++;
            }

            return chTable;
        }

        private int[] getNumOfNewChild(DateTime sday, DateTime eday)
        {
            int[] chTable = new int[context.Centers.Count()];

            int counter = 0;
            foreach (var c in context.Centers)
            {
                IEnumerable<Child> chs = context.Children.Where(ch => DateTime.Compare(ch.DateCreated, sday) >= 0 && DateTime.Compare(ch.DateCreated, eday) <= 0 && ch.PrimaryGuardian.CenterId == c.Id);
                chTable[counter] = chs.Count();
                counter++;
            }

            return chTable;
        }

        private string[,] getStringPGTable(DateTime startDay, DateTime endDay)
        {

            Center[] center = context.Centers.ToArray();
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
            foreach (Center c in context.Centers)
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
            string[,] languageString = new string[disLanguage.Length + 1, context.Centers.Count() +2];
            int cLength = context.Centers.Count();

            //first row
            languageString[0, 0] = "Language";
            int counter = 1;
            foreach (Center c in context.Centers)
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
            
            int cLength = context.Centers.Count();
            string[] disCountry = getCountrys(startDay, endDay);
            int[,] countryTable = getCountryTable(disCountry, startDay, endDay);
            string[,] countryString = new string[disCountry.Length + 1, cLength+2];

            //first row
            countryString[0, 0] = "Country";
            int counter = 1;
            foreach (Center c in context.Centers)
            {
                countryString[0, counter] = c.Name;
                counter++;
            }
            countryString[0, context.Centers.Count() + 1] = "Total";

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
            int[] pgTable = new int[context.Centers.Count() + 10];
            int counter = 0;
            foreach (var c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> pgs = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, eday) < 0 && pg.CenterId == c.Id);
                //IEnumerable<PrimaryGuardian> distinctPG = context.PrimaryGuardians.Distinct();
                pgTable[counter] = pgs.Count();
                counter++;
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
                if(pg.Language != null)
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
            int[,] countryTable = new int[disCountry.Length, context.Centers.Count()+1];

            int counter = 0;
            foreach (Center c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> aa = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
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
                for (int j = 0; j < context.Centers.Count(); j++)
                {
                    rowTotal += countryTable[i, j];
                }
                int length = context.Centers.Count();
                countryTable[i, length] = rowTotal;
            }

            return countryTable;
        }


        private int[,] getLanguageTable(string[] disLanguage, DateTime sday, DateTime eday)
        {
            //row means language, column means center
            int[,] languageTable = new int[disLanguage.Length, context.Centers.Count()+1];


            int counter = 0;
            foreach (Center c in context.Centers)
            {
                IEnumerable<PrimaryGuardian> aa = context.PrimaryGuardians.Where(pg => DateTime.Compare(pg.DateCreated, sday) > 0
                       && DateTime.Compare(pg.DateCreated, eday) < 0
                       && pg.CenterId == c.Id);
                for(int i = 0 ; i < disLanguage.Length; i++)
                {
                    languageTable[i, counter] = aa.Where(pg => pg.Language == disLanguage[i]).Count();
                }
                counter++;
            }


            for (int i = 0; i < disLanguage.Length; i++)
            {
                int rowTotal = 0;
                for (int j = 0; j < context.Centers.Count(); j++)
                {
                    rowTotal += languageTable[i, j];
                }
                int length = context.Centers.Count();
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

