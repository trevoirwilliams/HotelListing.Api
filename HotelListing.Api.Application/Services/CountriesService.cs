using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.Api.Application.Contracts;
using HotelListing.Api.Application.DTOs.Country;
using HotelListing.Api.Application.DTOs.Hotel;
using HotelListing.Api.Common.Constants;
using HotelListing.Api.Common.Models.Extensions;
using HotelListing.Api.Common.Models.Filtering;
using HotelListing.Api.Common.Models.Paging;
using HotelListing.Api.Common.Results;
using HotelListing.Api.Domain;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HotelListing.Api.Application.Services;

public class CountriesService(HotelListingDbContext context, IMapper mapper) : ICountriesService
{
    public async Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .ProjectTo<GetCountriesDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<IEnumerable<GetCountriesDto>>.Success(countries);
    }

    public async Task<Result<GetCountryDto>> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(q => q.CountryId == id)
            .ProjectTo<GetCountryDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return country is null
            ? Result<GetCountryDto>.Failure(new Error(ErrorCodes.NotFound, $"Country '{id}' was not found."))
            : Result<GetCountryDto>.Success(country);
    }

    public async Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createDto)
    {
        try
        {
            var exists = await CountryExistsAsync(createDto.Name);
            if (exists)
            {
                return Result<GetCountryDto>.Failure(new Error(ErrorCodes.Conflict, $"Country with name '{createDto.Name}' already exists."));
            }

            var country = mapper.Map<Country>(createDto);
            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var dto = await context.Countries
                .Where(c => c.CountryId == country.CountryId)
                .ProjectTo<GetCountryDto>(mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<GetCountryDto>.Success(dto);
        }
        catch
        {
            return Result<GetCountryDto>.Failure(new Error(ErrorCodes.Failure, "An unexpected error occurred while creating the country."));
        }
    }

    public async Task<Result> UpdateCountryAsync(int id, UpdateCountryDto updateDto)
    {
        try
        {
            if (id != updateDto.Id)
            {
                return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id."));
            }

            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country '{id}' was not found."));
            }

            // Use AutoMapper to map incoming DTO onto the tracked entity
            mapper.Map(updateDto, country);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch
        {
            return Result.Failure(new Error(ErrorCodes.Failure, "An unexpected error occurred while updating the country."));
        }
    }

    public async Task<Result> DeleteCountryAsync(int id)
    {
        try
        {
            var country = await context.Countries.FindAsync(id);
            if (country is null)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country '{id}' was not found."));
            }

            context.Countries.Remove(country);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch
        {
            return Result.Failure(new Error(ErrorCodes.Failure, "An unexpected error occurred while deleting the country."));
        }
    }

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == id);
    }

    public async Task<bool> CountryExistsAsync(string name)
    {
        return await context.Countries
            .AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<Result<PagedResult<GetCountriesDto>>> GetCountryHotelsAsync(int countryId, PaginationParameters paginationParameters, CountryFilterParameters filters)
    {
        var exists = await CountryExistsAsync(countryId);
        if (!exists)
        {
            return Result<PagedResult<GetCountriesDto>>.Failure(
                new Error(ErrorCodes.NotFound, $"Country '{countryId}' was not found."));
        }

        var query = context.Countries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.CountryName))
        {
            var name = filters.CountryName.Trim();
            query = query.Where(c => EF.Functions.Like(c.Name, $"%{name}%"));
        }

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            var term = filters.Search.Trim();
            query = query.Where(c =>
                EF.Functions.Like(c.Name, $"%{term}%") ||
                EF.Functions.Like(c.ShortName, $"%{term}%"));
        }

        if (filters.HasHotels.HasValue)
        {
            query = query.Where(c => c.Hotels.Any() == filters.HasHotels.Value);
        }

        // Sorting
        query = (filters.SortBy?.Trim().ToLowerInvariant()) switch
        {
            "name" => filters.SortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
            "shortname" => filters.SortDescending ? query.OrderByDescending(c => c.ShortName) : query.OrderBy(c => c.ShortName),
            "hotelcount" => filters.SortDescending ? query.OrderByDescending(c => c.Hotels.Count) : query.OrderBy(c => c.Hotels.Count),
            _ => query.OrderBy(c => c.Name) // default
        };

        var paged = await query
            .ProjectTo<GetCountriesDto>(mapper.ConfigurationProvider)
            .ToPagedResultAsync(paginationParameters);

        return Result<PagedResult<GetCountriesDto>>.Success(paged);
    }
}