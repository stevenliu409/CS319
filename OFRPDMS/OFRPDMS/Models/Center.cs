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
    
    public partial class Center
    {
        public Center()
        {
            this.Events = new HashSet<Event>();
            this.CenterReferrals = new HashSet<CenterReferral>();
            this.CenterAccounts = new HashSet<CenterAccount>();
            this.CenterFreeResources = new HashSet<CenterFreeResource>();
            this.PrimaryGuardians = new HashSet<PrimaryGuardian>();
            this.SpecialEvents = new HashSet<SpecialEvent>();
            this.LibraryResources = new HashSet<LibraryResource>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<CenterReferral> CenterReferrals { get; set; }
        public virtual ICollection<CenterAccount> CenterAccounts { get; set; }
        public virtual ICollection<CenterFreeResource> CenterFreeResources { get; set; }
        public virtual ICollection<PrimaryGuardian> PrimaryGuardians { get; set; }
        public virtual ICollection<SpecialEvent> SpecialEvents { get; set; }
        public virtual ICollection<LibraryResource> LibraryResources { get; set; }
    }
}
