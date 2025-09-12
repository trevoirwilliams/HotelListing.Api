using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Application.DTOs.Country;

public class CreateCountryDto
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [Required]
    [MaxLength(3)]
    public required string ShortName { get; set; }
}