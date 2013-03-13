using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFRPDMS.Areas.Admin.Models
{
    public class Report
    {
        public int[] CenterList { get; set; }
        public DateTime startDay { get; set; }
        public DateTime endDay { get; set; }
    }
}