using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.Configurations
{
    public class TransportationConfiguration : IEntityTypeConfiguration<Transportation>
    {
        public void Configure(EntityTypeBuilder<Transportation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.TransportationStatus)
               .HasConversion<string>() 
               .IsRequired();

            //builder.HasOne(x => x.Order)
            // .WithOne()
            // .HasForeignKey<Transportation>(x => x.OrderId)
            // .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(x => x.Cargos)
            //    .WithOne(x => x.Transportation)
            //    .HasForeignKey(x => x.TransportationId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
