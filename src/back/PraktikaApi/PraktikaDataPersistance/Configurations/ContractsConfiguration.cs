using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.Configurations
{
    public class ContractsConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractType)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(c => c.Order)
                .WithMany(o => o.ContractList)
                .HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
