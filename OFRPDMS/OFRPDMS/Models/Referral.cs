//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OFRPDMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Referral
    {
        public int Id { get; set; }
        public int CenterReferralId { get; set; }
        public System.DateTime DateReferred { get; set; }
        public int CountReferred { get; set; }
    
        public virtual CenterReferral CenterReferral { get; set; }
    }
}
