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
    using System.ComponentModel.DataAnnotations;
    
    public partial class SecondaryGuardian
    {
        public int Id { get; set; }
        //[RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid First Name")]
        public string FirstName { get; set; }
        //[RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Last Name")]
        public string LastName { get; set; }
        public string RelationshipToChild { get; set; }

        public Nullable<long> Phone { get; set; }
        public int PrimaryGuardianId { get; set; }
        public Nullable<bool> Delete { get; set; }
    
        public virtual PrimaryGuardian PrimaryGuardian { get; set; }
    }
}
