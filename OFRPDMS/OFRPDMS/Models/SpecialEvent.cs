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
    
    public partial class SpecialEvent
    {
        public SpecialEvent()
        {
            this.EventParticipants = new HashSet<EventParticipant>();
        }
    
        public string Name { get; set; }
        public string GuestSpeaker { get; set; }
        public string GuestSpeakerType { get; set; }
        public int CenterId { get; set; }
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
        public virtual Center Center { get; set; }
    }
}
