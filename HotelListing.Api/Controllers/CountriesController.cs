using Asp.Versioning;
using HotelListing.Api.Application.Contracts;
using HotelListing.Api.Application.DTOs.Country;
using HotelListing.Api.Application.DTOs.Hotel;
using HotelListing.Api.Common.Constants;
using HotelListing.Api.Common.Models.Filtering;
using HotelListing.Api.Common.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelListing.Api.Controllers;

/// <summary>
/// Represents an API controller that manages country-related operations, including retrieving, creating, updating, and
/// deleting countries, as well as accessing associated hotels.
/// </summary>
/// <remarks>This controller provides endpoints for clients to interact with country data, supporting filtering,
/// pagination, and role-based authorization for administrative actions. API versioning and rate limiting are applied
/// via attributes. All endpoints require appropriate permissions, and some actions are restricted to users with the
/// Administrator role.</remarks>
/// <param name="countriesService">The service used to perform country-related business logic and data operations.</param>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[EnableRateLimiting(RateLimitingConstants.FixedPolicy)]
public class CountriesController(ICountriesService countriesService) : BaseApiController
{
    /// <summary>
    /// Retrieves a list of countries that match the specified filter criteria.
    /// </summary>
    /// <param name="filters">An optional set of parameters used to filter the list of countries. If null, all countries are returned.</param>
    /// <returns>An asynchronous operation that returns an <see cref="ActionResult{T}"/> containing a collection of <see
    /// cref="GetCountriesDto"/> objects representing the countries that match the filter criteria.</returns>
    // GET: api/Countries
    [HttpGet]
    [OutputCache(PolicyName = CacheConstants.AuthenticatedUserCachingPolicy)]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries(
        [FromQuery] CountryFilterParameters?  filters)
    {
        var result = await countriesService.GetCountriesAsync(filters);
        return ToActionResult(result);
    }

    /// <summary>
    /// Retrieves a paginated list of hotels for the specified country, applying optional filtering criteria.
    /// </summary>
    /// <param name="countryId">The unique identifier of the country for which to retrieve hotels.</param>
    /// <param name="paginationParameters">The pagination settings that determine the page size and number of results to return.</param>
    /// <param name="filters">Optional filtering criteria to apply to the list of hotels, such as name or rating filters.</param>
    /// <returns>An asynchronous operation that returns an HTTP action result containing a paginated list of hotels for the
    /// specified country. The result includes hotel details and pagination metadata.</returns>
    // GET: api/Countries/{id}/hotels
    [HttpGet("{countryId:int}/hotels")]
    public async Task<ActionResult<GetCountryHotelsDto>> GetCountryHotels(
        [FromRoute] int countryId,
        [FromQuery] PaginationParameters paginationParameters,
        [FromQuery] CountryFilterParameters filters)
    {
        var result = await countriesService.GetCountryHotelsAsync(countryId, paginationParameters, filters);
        return ToActionResult(result);
    }

    /// <summary>
    /// Retrieves the details of a country with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the country to retrieve.</param>
    /// <returns>An asynchronous operation that returns an ActionResult containing a GetCountryDto with the country details if
    /// found; otherwise, a NotFound result.</returns>
    /// <response code="200"></response>
    /// <response code="404">Country not found</response>
    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var result = await countriesService.GetCountryAsync(id);
        return ToActionResult(result);
    }

    /// <summary>
    /// Updates the details of an existing country with the specified identifier.
    /// </summary>
    /// <remarks>This action requires the caller to have the Administrator role. The country is identified by
    /// the provided <paramref name="id"/>. If the country does not exist, a 404 Not Found response is
    /// returned.</remarks>
    /// <param name="id">The unique identifier of the country to update.</param>
    /// <param name="updateDto">An object containing the updated country information.</param>
    /// <returns>An <see cref="IActionResult"/> that represents the result of the update operation. Returns <see
    /// cref="NoContentResult"/> if the update is successful; otherwise, returns an appropriate error response.</returns>
    // PUT: api/Countries/5
    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateDto)
    {
        var result = await countriesService.UpdateCountryAsync(id, updateDto);
        return ToActionResult(result);
    }

    /// <summary>
    /// Applies a partial update to the country with the specified identifier using a JSON Patch document.
    /// </summary>
    /// <remarks>This action requires the caller to have the Administrator role. The patch document must
    /// conform to the structure of <see cref="UpdateCountryDto"/>. Only the fields specified in the patch document will
    /// be updated.</remarks>
    /// <param name="id">The unique identifier of the country to update.</param>
    /// <param name="patchDoc">The JSON Patch document containing the set of operations to apply to the country. Cannot be null.</param>
    /// <returns>An <see cref="IActionResult"/> that represents the result of the patch operation. Returns <see cref="OkResult"/>
    /// if the update is successful, <see cref="BadRequestResult"/> if the patch document is invalid, or an appropriate
    /// error result if the operation fails.</returns>
    // PATCH: api/Countries/5
    [HttpPatch("{id}")]
    [Authorize(Roles = RoleNames.Administrator)]
    public async Task<IActionResult> PatchCountry(int id, [FromBody] JsonPatchDocument<UpdateCountryDto> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest("Patch document is required.");
        }

        var result = await countriesService.PatchCountryAsync(id, patchDoc);
        return ToActionResult(result);
    }

    /// <summary>
    /// Creates a new country using the specified data.
    /// </summary>
    /// <remarks>This action requires the caller to have the Administrator role. On success, the response
    /// includes a Location header with the URI of the newly created country resource.</remarks>
    /// <param name="createDto">The data used to create the new country. Must not be null.</param>
    /// <returns>An ActionResult containing the created country data if the operation succeeds; otherwise, an error response
    /// describing the failure.</returns>
    // POST: api/Countries
    [HttpPost]
    [Authorize(Roles = RoleNames.Administrator)]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCountryDto createDto)
    {
        var result = await countriesService.CreateCountryAsync(createDto);
        if (!result.IsSuccess) return MapErrorsToResponse(result.Errors);

        return CreatedAtAction(nameof(GetCountry), new { id = result.Value!.Id }, result.Value);
    }

    /// <summary>
    /// Deletes the country with the specified identifier.
    /// </summary>
    /// <remarks>This action requires the caller to have the Administrator role. Only users with appropriate
    /// authorization can perform this operation.</remarks>
    /// <param name="id">The unique identifier of the country to delete.</param>
    /// <returns>An IActionResult that indicates the result of the delete operation. Returns a success response if the country
    /// was deleted; otherwise, returns an error response if the country was not found or the operation failed.</returns>
    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Administrator)]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var result = await countriesService.DeleteCountryAsync(id);
        return ToActionResult(result);
    }
}