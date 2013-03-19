using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.Models
{
    public class CenterFreeResourcesEdit
    {
        public IEnumerable<CenterFreeResource> CenterFreeResources { set; get; }
        public CenterFreeResourcesEdit()
        {
            if (CenterFreeResources == null)
                CenterFreeResources = new List<CenterFreeResource>();
        }
    }
}