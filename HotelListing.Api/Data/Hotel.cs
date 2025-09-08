using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Data;

public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public double Rating { get; set; }
    public decimal PerNightRate { get; set; }

    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public ICollection<HotelAdmin> Admins { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}
