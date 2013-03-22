using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;

namespace OFRPDMS.Areas.Staff.ViewModels
{
    public class ReferralViewModel
    {
        public CenterReferral referral { get; set; }
        public int count { get; set; }
        public int totalNumberMade { get; set; }
    }
}