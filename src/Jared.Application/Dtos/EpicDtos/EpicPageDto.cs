using Jared.Application.Dtos.PageDtos;

namespace Jared.Application.Dtos.EpicDtos;

public class EpicPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<EpicListDto> Epics { get; set; } = new();
}
