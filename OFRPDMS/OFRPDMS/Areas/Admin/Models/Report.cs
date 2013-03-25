using System;
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
            
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDay { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> dstart { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> dend { get; set; }
    }

    public class ReportDBContext : DbContext
    {
        public DbSet<Report> Report { get; set; }
    }
}