using HotelListing.Api.Application.Contracts;
using HotelListing.Api.Common.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace HotelListing.Api.Handlers;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IApiKeyValidatorService apiKeyValidatorService
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string apiKey = string.Empty;

        if (Request.Headers.TryGetValue(AuthenticationDefaults.ApiKeyHeaderName, out var headerValues))
        {
            apiKey = headerValues.ToString();
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.NoResult();
        }

        var valid = await apiKeyValidatorService.IsValidAsync(apiKey, Context.RequestAborted);
        if (!valid)
        {
            return AuthenticateResult.Fail("Invalid API key.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "apikey"),
            new(ClaimTypes.Name, "ApiKeyClient")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}

