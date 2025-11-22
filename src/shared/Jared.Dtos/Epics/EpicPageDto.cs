using Jared.Shared.Dtos.PageDtos;

namespace Jared.Dtos.Epics;

public class EpicPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<EpicListDto> Epics { get; set; } = new();
}
