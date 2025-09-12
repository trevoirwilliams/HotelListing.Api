using HotelListing.Api.Common.Constants;
using HotelListing.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelListing.Api.AuthorizationFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class HotelOrSystemAdminAttribute : TypeFilterAttribute
{
    public HotelOrSystemAdminAttribute() : base(typeof(HotelOrSystemAdminFilter))
    {
    }
}

public class HotelOrSystemAdminFilter(HotelListingDbContext dbContext) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpUser = context.HttpContext.User;

        if (httpUser?.Identity?.IsAuthenticated == false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // If user is a global Administrator, allow immediately
        if (httpUser!.IsInRole(RoleNames.Administrator))
        {
            return;
        }

        var userId = httpUser.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            context.Result = new ForbidResult();
            return;
        }

        // Try to get hotelId from route values
        context.RouteData.Values.TryGetValue("hotelId", out var hotelIdObj);
        int.TryParse(hotelIdObj?.ToString(), out int hotelId);
        if (hotelId == 0)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Check if user is an admin for this specific hotel
        var isHotelAdminUser = await dbContext.HotelAdmins
            .AnyAsync(q => q.UserId == userId && q.HotelId == hotelId);

        if (!isHotelAdminUser)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}