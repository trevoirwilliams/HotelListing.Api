using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Application.DTOs.Hotel;

public class UpdateHotelDto : CreateHotelDto
{
    [Required]
    public int Id { get; set; }
}