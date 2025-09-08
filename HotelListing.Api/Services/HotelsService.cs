using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelListing.Api.Services;

public class HotelsService(HotelListingDbContext context, 
    ICountriesService countriesService, 
    IMapper mapper) : IHotelsService
{
    public async Task<Result<IEnumerable<GetHotelDto>>> GetHotelsAsync()
    {
        var test = context.Hotels.ToList();
        var hotels = await context.Hotels
            .Include(q => q.Country)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<IEnumerable<GetHotelDto>>.Success(hotels);
    }

    public async Task<Result<GetHotelDto>> GetHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (hotel is null)
        {
            return Result<GetHotelDto>.Failure(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
        }

        return Result<GetHotelDto>.Success(hotel);
    }

    public async Task<Result<GetHotelDto>> CreateHotelAsync(CreateHotelDto hotelDto)
    {
        var countryExists = await countriesService.CountryExistsAsync(hotelDto.CountryId);
        if (!countryExists)
        {
            return Result<GetHotelDto>.Failure(new Error(ErrorCodes.NotFound, $"Country '{hotelDto.CountryId}' was not found."));
        }

        var duplicate = await HotelExistsAsync(hotelDto.Name, hotelDto.CountryId);
        if (duplicate)
        {
            return Result<GetHotelDto>.Failure(new Error(ErrorCodes.Conflict, $"Hotel '{hotelDto.Name}' already exists in the selected country."));
        }

        var hotel = mapper.Map<Hotel>(hotelDto);
        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        var dto = await context.Hotels
            .Where(h => h.Id == hotel.Id)
            .ProjectTo<GetHotelDto>(mapper.ConfigurationProvider)
            .FirstAsync();

        return Result<GetHotelDto>.Success(dto);
    }

    public async Task<Result> UpdateHotelAsync(int id, UpdateHotelDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id."));
        }

        var hotel = await context.Hotels.FindAsync(id);
        if (hotel is null)
        {
            return Result.NotFound(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
        }

        var countryExists = await countriesService.CountryExistsAsync(updateDto.CountryId);
        if (!countryExists)
        {
            return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country '{updateDto.CountryId}' was not found."));
        }

        mapper.Map(updateDto, hotel);

        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteHotelAsync(int id)
    {
        var affected = await context.Hotels
            .Where(q => q.Id == id)
            .ExecuteDeleteAsync();

        if (affected == 0)
        {
            return Result.NotFound(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
        }

        return Result.Success();
    }

    public async Task<bool> HotelExistsAsync(int id)
    {
        return await context.Hotels.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> HotelExistsAsync(string name, int countryId)
    {
        return await context.Hotels
            .AnyAsync(e => e.Name.ToLower().Trim() == name.ToLower().Trim() && e.CountryId == countryId);
    }
}