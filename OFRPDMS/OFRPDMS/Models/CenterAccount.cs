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
    
    public partial class CenterAccount
    {
        public CenterAccount()
        {
            this.Accounts = new HashSet<Account>();
        }
    
        public int Id { get; set; }
        public int CenterId { get; set; }
    
        public virtual Center Center { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
