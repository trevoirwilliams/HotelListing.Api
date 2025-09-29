using HotelListing.Api.Common.Enums;

namespace HotelListing.Api.Common.Models.Filtering;

public class BookingFilterParameters : BaseFilterParameters
{
    public BookingStatus? Status { get; set; }
    public DateOnly? CheckInFrom { get; set; }
    public DateOnly? CheckInTo { get; set; }
    public DateOnly? CheckOutFrom { get; set; }
    public DateOnly? CheckOutTo { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinGuests { get; set; }
    public int? MaxGuests { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
}
