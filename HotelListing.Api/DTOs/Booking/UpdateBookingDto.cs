using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Booking;

public record UpdateBookingDto(
    DateOnly CheckIn,
    DateOnly CheckOut,
    [Required][Range(minimum: 1, maximum: 10)] int Guests
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CheckOut <= CheckIn)
        {
            yield return new ValidationResult(
                "Check-out must be after check-in.",
                [nameof(CheckOut), nameof(CheckIn)]);
        }
    }
}
