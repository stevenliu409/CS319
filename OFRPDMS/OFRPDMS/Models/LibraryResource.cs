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
    
    public partial class LibraryResource
    {
        public LibraryResource()
        {
            this.PrimaryGuardianBorrows = new HashSet<PrimaryGuardianBorrow>();
        }
    
        public int Id { get; set; }
        public Nullable<bool> Broken { get; set; }
        public Nullable<bool> CheckedOut { get; set; }
        public Nullable<decimal> Value { get; set; }
        public byte[] Image { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public Nullable<bool> Sanitized { get; set; }
        public int CenterId { get; set; }
    
        public virtual ICollection<PrimaryGuardianBorrow> PrimaryGuardianBorrows { get; set; }
        public virtual Center Center { get; set; }
    }
}
