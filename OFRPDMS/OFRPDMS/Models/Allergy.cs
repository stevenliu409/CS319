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
    
    public partial class Allergy
    {
        public int Id { get; set; }
        public int PrimaryGuardianId { get; set; }
        public int ChildId { get; set; }
        public string Note { get; set; }
        public Nullable<bool> Delete { get; set; }
    
        public virtual PrimaryGuardian PrimaryGuardian { get; set; }
        public virtual Child Child { get; set; }
    }
}
