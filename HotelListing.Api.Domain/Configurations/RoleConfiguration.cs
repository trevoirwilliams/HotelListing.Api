using HotelListing.Api.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Domain.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "c78e8f15-6a6c-4c8a-b5d1-98394b071953",
                Name = RoleNames.Administrator,
                NormalizedName = RoleNames.Administrator.ToUpper()
            },
            new IdentityRole
            {
                Id = "36aac992-72ff-4527-9008-52e7c145ca39",
                Name = RoleNames.User,
                NormalizedName = RoleNames.User.ToUpper()
            },
            new IdentityRole
            {
                Id = "36aac992-4c8a-4527-9008-98394b071953",
                Name = RoleNames.HotelAdmin,
                NormalizedName = RoleNames.HotelAdmin.ToUpper()
            }
        );
    }
}

