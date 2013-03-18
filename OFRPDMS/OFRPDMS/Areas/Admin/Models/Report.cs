using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OFRPDMS.Areas.Admin.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime startDay { get; set; }
        public DateTime endDay { get; set; }
    }

    public class ReportDBContext : DbContext
    {
        public DbSet<Report> Report { get; set; }
    }
}