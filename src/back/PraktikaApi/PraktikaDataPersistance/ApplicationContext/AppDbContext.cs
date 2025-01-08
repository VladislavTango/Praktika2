using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using PraktikaDomain.Entities;
using PraktikaDomain.Entities.TransportEntities;

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
        public DbSet<Trailer> Trailers { get; set; } = null;
        public DbSet<Truck> Trucks { get; set; } = null;
        public DbSet<Vehicle> Vehicles { get; set; } = null;
        public DbSet<CargoTrailerCompatibility> cargoTrailerCompatibilities { get; set; } = null;

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
