using BCP.Core.Entities;
using BCP.Core.Enums;
using BCP.Infrastructure.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCP.Infrastructure.Configurations;

public class RentEntryConfiguration : IEntityTypeConfiguration<RentEntry>
{
    public void Configure(EntityTypeBuilder<RentEntry> builder)
    {
        _ = builder.ToTable("rental_entries");

        _ = builder.Property(r => r.Status).HasConversion(new EnumConverter<BikeRentalStatus>());
    }
}