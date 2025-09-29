using HotelListing.Api.Application.Contracts;
using HotelListing.Api.Application.DTOs.Booking;
using HotelListing.Api.AuthorizationFilters;
using HotelListing.Api.Common.Models.Filtering;
using HotelListing.Api.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/hotels/{hotelId:int}/bookings")]
[ApiController]
[Authorize]
public class HotelBookingsController(IBookingService bookingService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<GetBookingDto>>> GetBookings(
        [FromRoute] int hotelId,
        [FromQuery] PaginationParameters paginationParameters,
        [FromQuery] BookingFilterParameters filters)
    {
        var result = await bookingService.GetUserBookingsForHotelAsync(hotelId, paginationParameters, filters);
        return ToActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<GetBookingDto>> CreateBooking([FromRoute] int hotelId, [FromBody] CreateBookingDto createBookingDto)
    {
        var result = await bookingService.CreateBookingAsync(createBookingDto);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}")]
    public async Task<ActionResult<GetBookingDto>> UpdateBooking(
    [FromRoute] int hotelId,
    [FromRoute] int bookingId,
    [FromBody] UpdateBookingDto updateBookingDto)
    {
        var result = await bookingService.UpdateBookingAsync(hotelId, bookingId, updateBookingDto);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/cancel")]
    public async Task<IActionResult> CancelBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.CancelBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }

    [HttpGet("admin")]
    [HotelOrSystemAdmin]
    public async Task<ActionResult<PagedResult<GetBookingDto>>> GetBookingsAdmin(
        [FromRoute] int hotelId,
        [FromQuery] PaginationParameters paginationParameters,
        [FromQuery] BookingFilterParameters filters)
    {
        var result = await bookingService.GetBookingsForHotelAsync(hotelId, paginationParameters, filters);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/admin/cancel")]
    [HotelOrSystemAdmin]
    public async Task<IActionResult> AdminCancelBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.AdminCancelBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/admin/confirm")]
    [HotelOrSystemAdmin]
    public async Task<IActionResult> AdminConfirmBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.AdminConfirmBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }
}
