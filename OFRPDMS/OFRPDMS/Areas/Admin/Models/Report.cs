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

        public System.DateTime startDay { get; set; }

        public System.DateTime endDay { get; set; }


        public System.DateTime startDay2 { get; set; }

        public System.DateTime endDay2 { get; set; }


        public System.DateTime startDay3 { get; set; }

        public System.DateTime endDay3 { get; set; }

        public int pgid { get; set; }
        public string type { get; set; } 

    }

    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
    }

}