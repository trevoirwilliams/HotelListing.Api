using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Data.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasIndex(k => k.Key).IsUnique();
        builder.HasData(
            new ApiKey
            {
                Id = 1,
                AppName = "app",
                CreatedAtUtc = new DateTime(2025, 01, 01),
                Key = "dXNlcjFAbG9jYWxob3N0LmNvbTpQQHNzrbd29yZDE="

            }
        );
    }
}
