using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TheaterApp.Models
{
    public class Context : DbContext
    {
        public Context() : base("Context")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Movie> Movies { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}