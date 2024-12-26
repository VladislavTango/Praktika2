using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.Configurations
{
    public class InovicesConfigureation : IEntityTypeConfiguration<Invoices>
    {
        public void Configure(EntityTypeBuilder<Invoices> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Transportation)
                   .WithOne()
                   .HasForeignKey<Invoices>(i => i.TransportationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(i => i.Status)
                   .HasDefaultValue(true);
        }
    }
}
