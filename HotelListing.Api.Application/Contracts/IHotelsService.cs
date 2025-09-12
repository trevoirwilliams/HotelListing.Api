using HotelListing.Api.Application.DTOs.Hotel;
using HotelListing.Api.Common.Results;

namespace HotelListing.Api.Application.Contracts;

public interface IHotelsService
{
    // Keep these for quick checks elsewhere if needed
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name, int countryId);
    Task<Result<IEnumerable<GetHotelDto>>> GetHotelsAsync();
    Task<Result<GetHotelDto>> GetHotelAsync(int id);
    Task<Result<GetHotelDto>> CreateHotelAsync(CreateHotelDto createDto);
    Task<Result> UpdateHotelAsync(int id, UpdateHotelDto updateDto);
    Task<Result> DeleteHotelAsync(int id);
}