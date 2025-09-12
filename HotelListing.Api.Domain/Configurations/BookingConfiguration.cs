using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Domain.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(q => q.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.HotelId);
        builder.HasIndex(x => new { x.CheckIn, x.CheckOut });
    }
}
