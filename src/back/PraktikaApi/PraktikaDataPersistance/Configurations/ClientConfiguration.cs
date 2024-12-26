using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PraktikaDomain.Entities;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasMany(x => x.OrderList) 
            .WithOne(x => x.Client) 
            .HasForeignKey(x => x.ClientId) 
            .OnDelete(DeleteBehavior.Cascade);
    }
}
