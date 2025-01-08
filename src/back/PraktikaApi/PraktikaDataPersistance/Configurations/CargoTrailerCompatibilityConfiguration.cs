using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PraktikaDomain.Entities;
using PraktikaDomain.Enums;

namespace PraktikaDataPersistance.Configurations
{
    public class CargoTrailerCompatibilityConfiguration : IEntityTypeConfiguration<CargoTrailerCompatibility>
    {
        public void Configure(EntityTypeBuilder<CargoTrailerCompatibility> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CargoType)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.TrailerType)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasData(
            new CargoTrailerCompatibility { Id = 1, CargoType = CargoType.STANDART, TrailerType = TrailerType.DEFAULT },
            new CargoTrailerCompatibility { Id = 2, CargoType = CargoType.DANGER, TrailerType = TrailerType.DEFAULT },
            new CargoTrailerCompatibility { Id = 3, CargoType = CargoType.DANGER, TrailerType = TrailerType.BIG },
            new CargoTrailerCompatibility { Id = 4, CargoType = CargoType.DANGER, TrailerType = TrailerType.LIQUID },
            new CargoTrailerCompatibility { Id = 5, CargoType = CargoType.BIG, TrailerType = TrailerType.BIG },
            new CargoTrailerCompatibility { Id = 6, CargoType = CargoType.LIQUID, TrailerType = TrailerType.LIQUID }
            );
        }
    }
}
