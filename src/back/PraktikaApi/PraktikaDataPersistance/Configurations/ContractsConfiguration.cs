using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;

namespace PraktikaDataPersistance.Configurations
{
    public class ContractsConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Order)
            .WithMany(x => x.ContractList)  
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);   
        }
    }
}
