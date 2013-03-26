﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace OFRPDMS.Areas.Admin.Models
{
    
    public class Report
    {

        public Report()
        {
            dstart = null;
            dend = null;
            cstart = null;
            cend = null;
            
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDay { get; set; }
        public int pgid { get; set; }
        public string type { get; set; } 
        [DataType(DataType.Date)]
        public Nullable<DateTime> dstart { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> dend { get; set; }
        public Nullable<DateTime> cstart { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> cend { get; set; }
    }

    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
    }

}