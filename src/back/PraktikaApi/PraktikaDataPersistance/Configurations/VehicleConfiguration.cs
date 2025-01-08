using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaDataPersistance.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.HasOne(v => v.Truck)
               .WithOne(t => t.Vehicle)
               .HasForeignKey<Truck>(t => t.VehicleId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(v => v.Trailer)
                .WithOne(t => t.Vehicle)
                .HasForeignKey<Trailer>(t => t.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
