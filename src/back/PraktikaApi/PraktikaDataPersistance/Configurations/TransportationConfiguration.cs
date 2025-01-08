using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.Configurations
{
    public class TransportationConfiguration : IEntityTypeConfiguration<Transportation>
    {
        public void Configure(EntityTypeBuilder<Transportation> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(o => o.CargoType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.TransportationStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(t => t.Order)
                .WithMany(o => o.TransportationList)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
