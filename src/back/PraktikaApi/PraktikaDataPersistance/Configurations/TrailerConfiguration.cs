using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities.TransportEntities;

namespace PraktikaDataPersistance.Configurations
{
    public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
    {
        public void Configure(EntityTypeBuilder<Trailer> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrailerType)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(t => t.Vehicle)
                .WithOne(v => v.Trailer)
                .HasForeignKey<Trailer>(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
