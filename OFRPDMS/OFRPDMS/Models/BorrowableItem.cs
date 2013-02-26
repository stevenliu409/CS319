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
    
    public partial class BorrowableItem
    {
        public int Id { get; set; }
        public Nullable<bool> IsDefective { get; set; }
        public bool InInventory { get; set; }
        public Nullable<decimal> Value { get; set; }
        public byte[] Image { get; set; }
        public string Note { get; set; }
        public Nullable<int> LendingPeriodDays { get; set; }
        public string ItemType { get; set; }
        public int PrimaryGuardianBorrowId { get; set; }
        public int CenterId { get; set; }
    
        public virtual PrimaryGuardianBorrow PrimaryGuardianBorrow { get; set; }
        public virtual Center Center { get; set; }
    }
}
