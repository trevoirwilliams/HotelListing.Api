namespace HotelListing.Api.DTOs.Hotel;

public record GetHotelDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId,
    string CountryName);

public record GetHotelSlimDto(
    int Id,
    string Name,
    string Address,
    double Rating
);
