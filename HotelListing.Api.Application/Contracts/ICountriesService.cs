using HotelListing.Api.Application.DTOs.Country;
using HotelListing.Api.Common.Results;

namespace HotelListing.Api.Application.Contracts;

public interface ICountriesService
{
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
    Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createDto);
    Task<Result> DeleteCountryAsync(int id);
    Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync();
    Task<Result<GetCountryDto>> GetCountryAsync(int id);
    Task<Result> UpdateCountryAsync(int id, UpdateCountryDto updateDto);
}