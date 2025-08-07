namespace HotelListing.Api.DTOs.Hotel;

public record GetHotelsDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId
);

public record GetHotelSlimDto(
    int Id,
    string Name,
    string Address,
    double Rating
);
