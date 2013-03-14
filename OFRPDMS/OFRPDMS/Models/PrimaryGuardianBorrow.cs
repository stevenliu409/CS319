//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace OFRPDMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrimaryGuardianBorrow
    {
        public int Id { get; set; }
        public System.DateTime BorrowedDate { get; set; }
        public int PrimaryGuardianId { get; set; }
        public bool Returned { get; set; }
    
        public virtual PrimaryGuardian PrimaryGuardian { get; set; }

        // required because of 1 to 1 relationship with a LibraryItem
        [Required]
        public virtual LibraryItem LibraryItem { get; set; }
    }
}
