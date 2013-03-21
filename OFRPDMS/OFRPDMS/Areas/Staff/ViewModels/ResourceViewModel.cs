using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.ViewModels
{
    public class ResourceViewModel
    {
        public CenterFreeResource resource { get; set; }
        public int count { get; set; }
        public int totalHandedOut { get; set; }
    }
}