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
    
    public partial class EventParticipant
    {
        public EventParticipant()
        {
            this.PrimaryGuardians = new HashSet<PrimaryGuardian>();
            this.Children = new HashSet<Child>();
        }
    
        public int Id { get; set; }
        public int EventId { get; set; }
    
        public virtual ICollection<PrimaryGuardian> PrimaryGuardians { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual Event Event { get; set; }
    }
}