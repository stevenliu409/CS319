using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using OFRPDMS.Areas.Staff.ViewModels;

namespace OFRPDMS.Areas.Staff.Models
{
    public class ReferralViewModelsEdit
    {
        public IEnumerable<ReferralViewModel> Referrals { set; get; }
        public ReferralViewModelsEdit()
        {
            if (Referrals == null)
                Referrals = new List<ReferralViewModel>();
        }
    }
}