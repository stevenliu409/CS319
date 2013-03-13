using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OFRPDMS.Models
{
    public class OFRPDMSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<OFRPDMS.Models.OFRPDMSContext>());
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<PrimaryGuardianBorrow> PrimaryGuardianBorrows { get; set; }
        public DbSet<PrimaryGuardian> PrimaryGuardians { get; set; }
        public DbSet<SecondaryGuardian> SecondaryGuardians { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<CenterReferral> CenterReferrals { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<CenterAccount> CenterAccounts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CenterFreeResource> CenterFreeResources { get; set; }
        public DbSet<SpecialEvent> SpecialEvents { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<GivenResource> GivenResources { get; set; }
    }
}