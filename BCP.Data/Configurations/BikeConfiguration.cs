using BCP.Core.Entities;
using BCP.Core.Enums;
using BCP.Infrastructure.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCP.Data.Configurations
{
    public class BikeConfiguration : IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            builder.ToTable("Bikes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(b => b.RentalStatus).HasConversion(new EnumConverter<BikeRentalStatus>());

            builder.Property(x => x.NumberPlate)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Rating)
                .HasColumnType("decimal(3,2)");

            builder.Property(x => x.KmRun)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Milage)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);
            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            builder.HasOne(x => x.Brand)
                .WithMany()
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
