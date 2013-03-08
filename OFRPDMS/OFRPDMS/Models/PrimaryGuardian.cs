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
    
    public partial class PrimaryGuardian
    {
        public PrimaryGuardian()
        {
            this.PrimaryGuardianBorrows = new HashSet<PrimaryGuardianBorrow>();
            this.EventParticipants = new HashSet<EventParticipant>();
            this.Children = new HashSet<Child>();
            this.Allergies = new HashSet<Allergy>();
        }
    
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$")] 
        public string Email { get; set; }
        [Required]
        public Nullable<int> Phone { get; set; }
        [Required]
        public string PostalCodePrefix { get; set; }
        [Required]
        public string DateCreated { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string RelationshipToChild { get; set; }
    
        public virtual ICollection<PrimaryGuardianBorrow> PrimaryGuardianBorrows { get; set; }
        public virtual SecondaryGuardian SecondaryGuardian { get; set; }
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<Allergy> Allergies { get; set; }
    }
}
