namespace Jared.Dtos.Epics;

public class EpicPageDto : IPagination<EpicListDto>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<EpicListDto> Items { get; set; } = new();
}
