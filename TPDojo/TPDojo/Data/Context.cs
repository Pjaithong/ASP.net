using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TPDojoBO;

namespace TPDojo.Data
{
    public class Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Context() : base("name=Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtMartials).WithMany();
            modelBuilder.Entity<Samourai>().HasOptional(x => x.Arme);
            base.OnModelCreating(modelBuilder);
        }
        public System.Data.Entity.DbSet<TPDojoBO.Arme> Armes { get; set; }

        public System.Data.Entity.DbSet<TPDojoBO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<TPDojoBO.ArtMartial> ArtMartials { get; set; }
    }
}
