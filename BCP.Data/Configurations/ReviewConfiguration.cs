using BCP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCP.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        builder.HasOne(x => x.Bike)
            .WithMany(x => x.Reviews).HasForeignKey(x => x.BikeId)
            .HasConstraintName("bike_id_fk");
    }
}