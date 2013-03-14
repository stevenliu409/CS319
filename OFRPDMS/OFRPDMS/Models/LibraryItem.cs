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
    
    public abstract partial class LibraryItem
    {
        public int Id { get; set; }
        public string Broken { get; set; }
        public string CheckedOut { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public string LendingPeriod { get; set; }
        public string Name { get; set; }
    
        public virtual PrimaryGuardianBorrow PrimaryGuardianBorrow { get; set; }

        [Required]
        public virtual Center Center { get; set; }
    }
}