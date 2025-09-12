namespace HotelListing.Api.Domain;

public class Country
{
    public int CountryId { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public IList<Hotel> Hotels { get; set; } = [];
}