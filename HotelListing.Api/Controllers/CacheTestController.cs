using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace HotelListing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CacheTestController : ControllerBase
{
    private readonly ILogger<CacheTestController> _logger;

    public CacheTestController(ILogger<CacheTestController> logger)
    {
        _logger = logger;
    }

    [HttpGet("simple")]
    [OutputCache(Duration = 60)] // Simple 60-second cache
    public IActionResult GetSimple()
    {
        _logger.LogWarning("🔴 GetSimple EXECUTED - This should only appear on cache MISS");
        
        return Ok(new 
        { 
            message = "Hello from cache test",
            timestamp = DateTime.UtcNow,
            random = Random.Shared.Next(1000, 9999)
        });
    }

    [HttpGet("with-policy")]
    [OutputCache(PolicyName = "TestPolicy")]
    public IActionResult GetWithPolicy()
    {
        _logger.LogWarning("🔴 GetWithPolicy EXECUTED - Cache MISS");
        
        return Ok(new 
        { 
            message = "Policy test",
            timestamp = DateTime.UtcNow
        });
    }
}