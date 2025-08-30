using Microsoft.AspNetCore.Http;

namespace HotelListing.Api.Constants;

public class AuthenticationDefaults
{
    public const string BasicScheme = "Basic";
    public const string ApiKeyScheme = "ApiKey";
    public const string ApiKeyHeaderName = "X-Api-Key";
    public const string AppName = "HotelListingApi";
}
