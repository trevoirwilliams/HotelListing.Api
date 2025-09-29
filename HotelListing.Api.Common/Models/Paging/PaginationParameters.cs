using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Common.Models.Paging;

public class PaginationParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    [Range(1, MaxPageSize, ErrorMessage = "Page size must be between 1 and 50")]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
