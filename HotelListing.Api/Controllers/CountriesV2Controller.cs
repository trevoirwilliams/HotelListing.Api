using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/v{version:apiVersion}/countries")]
[ApiController]
[ApiVersion("2.0", Deprecated = true)]
public class CountriesV2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult GetCountries(
        [FromQuery] int? pageNumber = 1, 
        [FromQuery] int? pageSize = 10)
    {
        // Version 2 implementation with pagination
        return Ok(new
        {
            Version = "2.0",
            Message = "Enhanced countries endpoint with pagination",
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }
}
