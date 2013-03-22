using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.ViewModels;

namespace OFRPDMS.Areas.Staff.Models
{
    public class ResourceViewModelsEdit
    {
        public IEnumerable<ResourceViewModel> Resources { set; get; }
        public ResourceViewModelsEdit()
        {
            if (Resources == null)
                Resources = new List<ResourceViewModel>();
        }
    }
}