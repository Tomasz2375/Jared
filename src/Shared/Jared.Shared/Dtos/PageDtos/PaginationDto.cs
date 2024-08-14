using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.PageDtos;

public class PaginationDto : IPagination
{
    public int ItemsCount { get; set; }
    public int ItemFrom { get; set; }
    public int ItemTo { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
}
