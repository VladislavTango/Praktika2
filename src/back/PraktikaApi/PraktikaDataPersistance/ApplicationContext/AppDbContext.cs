using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.ApplicationContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null;
        public DbSet<Contract> Contracts { get; set; } = null;
        public DbSet<Order> Orders { get; set; } = null;
        public DbSet<Transportation> Transportations { get; set; } = null;
        public DbSet<Cargo> Cargos { get; set; } = null;
        public DbSet<Invoices> Inovices { get; set; } = null;
        //public DbSet<OpenIddictEntityFrameworkCoreApplication> Applications { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreAuthorization> Authorizations { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreScope> Scopes { get; set; }
        //public DbSet<OpenIddictEntityFrameworkCoreToken> Tokens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
