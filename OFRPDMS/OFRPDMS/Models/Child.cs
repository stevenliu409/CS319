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
    
    public partial class Child
    {
        public Child()
        {
            this.EventParticipants = new HashSet<EventParticipant>();
            this.Allergies = new HashSet<Allergy>();
        }

        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid first Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Birthdate { get; set; }
        [Required]
        public int PrimaryGuardianId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid relationship")]
        public string RelationshipToGuardian { get; set; }
        public bool Delete { get; set; }

   
    
        public virtual PrimaryGuardian PrimaryGuardian { get; set; }
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
        public virtual ICollection<Allergy> Allergies { get; set; }
    }
}
