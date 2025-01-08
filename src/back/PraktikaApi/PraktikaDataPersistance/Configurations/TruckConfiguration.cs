using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaDataPersistance.Configurations
{
    public class TruckConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Vehicle)
            .WithOne(v => v.Truck)
            .HasForeignKey<Truck>(t => t.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
