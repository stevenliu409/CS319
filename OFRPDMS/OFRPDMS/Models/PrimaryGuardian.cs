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
    
    public partial class PrimaryGuardian
    {
        public PrimaryGuardian()
        {
            this.EventParticipants = new HashSet<EventParticipant>();
            this.Children = new List<Child>();
            this.Allergies = new List<Allergy>();
            this.SecondaryGuardians = new List<SecondaryGuardian>();
        }
    
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid first Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Not a valid Email")] 
        public string Email { get; set; }
    
        public Nullable<int> Phone { get; set; }
        public string PostalCodePrefix { get; set; }

        public System.DateTime DateCreated { get; set; }

        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Language Name")]
        public string Language { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Country Name")]
        public string Country { get; set; }

        public int CenterId { get; set; }
    
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<SecondaryGuardian> SecondaryGuardians { get; set; }
        public virtual Center Center { get; set; }
        public virtual IList<Child> Children { get; set; }


        public void BuildEntity(int count)
        {

            for (int i = 0; i < count; i++)
            {
                Children.Add(new Child());
                Allergies.Add(new Allergy());
                SecondaryGuardians.Add(new SecondaryGuardian());

            }

        }



    }
}
