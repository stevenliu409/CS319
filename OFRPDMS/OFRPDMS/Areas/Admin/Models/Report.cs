using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Admin.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime startDay { get; set; }
        public DateTime endDay { get; set; }
        public int pgid { get; set; }
    }

    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
    }

}