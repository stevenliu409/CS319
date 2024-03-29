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
           
           
            this.SecondaryGuardians = new List<SecondaryGuardian>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Not a valid Email")]
        public string Email { get; set; }

        [RegularExpression(@"^[\d -]+$",ErrorMessage= "Not a valid Phonoe Number")]
        public string Phone { get; set; }

        public string PostalCodePrefix { get; set; }
        public System.DateTime DateCreated { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Language Name")]
        public string Language { get; set; }
        [RegularExpression(@"^[a-zA-Z]{1,25}$", ErrorMessage = "Not a valid Country Name")]
        public string Country { get; set; }
        public int CenterId { get; set; }
        public string Allergies { get; set; }
    
        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
        public virtual IList<Child> Children { get; set; }
        //public virtual Allergy Allergies { get; set; }
        public virtual IList<SecondaryGuardian> SecondaryGuardians { get; set; }
        public virtual Center Center { get; set; }
        
        


        public void BuildEntity(int count)
        {

            for (int i = 0; i < count; i++)
            {
                //Children.Add(new Child());
                //Allergies.Add(new Allergy());
            
                //SecondaryGuardians.Add(new SecondaryGuardian());

            }

        }

        //// get the number of new PG(created after start date) 
        //public int[,] getNumOfNewPGTable(List<PrimaryGuardian> pg, List<Center> center, DateTime startDay, DateTime endDay)
        //{
        //    int[,] newPGTable = new int[5, 1];
        //    for (int i = 0; i < center.Count; i++)
        //    {
        //        newPGTable[i, 0] = (from u in pg
        //                            where u.DateCreated > startDay && u.Center.Id == i
        //                            select u).ToArray().Length;
        //    }
        //    return newPGTable;
        //}

        //private int[,] getNumOfPGTable()
        //{
        //    int[,] pgTable = new int[5, 1];
        //    for (int i = 0; i < context.Centers.ToArray().Length; i++)
        //    {
        //        pgTable[i, 0] = (from u in context.PrimaryGuardians
        //                         where u.Center.Id == i
        //                         select u).ToArray().Length;
        //    }
        //    return pgTable;
        //}


    }
}
