using HotelListing.Api.DTOs.Auth;
using HotelListing.Api.Results;

namespace HotelListing.Api.Contracts;

public interface IUsersService
{
    string UserId { get; }
    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}