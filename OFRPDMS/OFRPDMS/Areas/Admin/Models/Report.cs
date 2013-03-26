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

        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDay { get; set; }
        public int pgid { get; set; }
        public string type { get; set; } 
    }

    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
    }

}