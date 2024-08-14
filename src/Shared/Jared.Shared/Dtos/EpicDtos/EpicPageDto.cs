using Jared.Shared.Dtos.PageDtos;

namespace Jared.Shared.Dtos.EpicDtos;

public class EpicPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<EpicListDto> Epics { get; set; } = new();
}
