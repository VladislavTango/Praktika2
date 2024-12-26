using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.OrderList)
            .HasForeignKey(x => x.ClientId);
    }
}
