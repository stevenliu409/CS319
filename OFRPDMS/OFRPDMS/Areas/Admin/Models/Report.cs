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
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime startDay { get; set; }
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime endDay { get; set; }

        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime startDay2 { get; set; }
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime endDay2 { get; set; }

        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime startDay3 { get; set; }
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Not a valid Date")]
        public System.DateTime endDay3 { get; set; }

        public int pgid { get; set; }
        public string type { get; set; } 

    }

    public class ReportContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
    }

}