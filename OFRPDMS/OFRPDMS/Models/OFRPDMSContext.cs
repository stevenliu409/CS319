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
        public DbSet<OFRPDMS.Models.Center> Centers { get; set; }

        public DbSet<OFRPDMS.Models.PrimaryGuardian> PrimaryGuardians { get; set; }

        public DbSet<OFRPDMS.Models.SecondaryGuardian> SecondaryGuardians { get; set; }

        public DbSet<OFRPDMS.Models.Event> Events { get; set; }
    }
}