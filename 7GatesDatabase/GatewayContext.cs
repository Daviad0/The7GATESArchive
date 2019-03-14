using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using The7GATESArchive.Models;

namespace The7GATESArchive.DAL
{
    public class GatewayContext : DbContext
    {

        public GatewayContext() : base("GatewayContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGate> UserGates { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Update> Updates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}