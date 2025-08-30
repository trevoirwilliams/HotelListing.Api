using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HotelListing.Api.Handlers;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IUsersService usersService
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        // Basic Base64({username:password})
        var authHeader = authHeaderValues.ToString();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.NoResult();
        }

        var token = authHeader["Basic ".Length..].Trim();
        string decoded;

        try
        {
            var credentialBytes = Convert.FromBase64String(token);
            // {username:password}
            decoded = Encoding.UTF8.GetString(credentialBytes);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Basic authentication token.");
        }

        var separatorIndex = decoded.IndexOf(':');
        if (separatorIndex <= 0)
        {
            return AuthenticateResult.Fail("Invalid Basic authentication credentials format.");
        }

        var userNameOrEmail = decoded[..separatorIndex];
        var password = decoded[(separatorIndex + 1)..];

        var loginDto = new LoginUserDto
        {
            Email = userNameOrEmail,
            Password = password
        };

        var result = await usersService.LoginAsync(loginDto);
        if (!result.IsSuccess) 
        {
            return AuthenticateResult.Fail("Invalid username or password.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userNameOrEmail),
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}

