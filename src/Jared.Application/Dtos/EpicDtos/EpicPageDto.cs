using Jared.Application.Dtos.PageDtos;

namespace Jared.Application.Dtos.EpicDtos;

public class EpicPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public IEnumerable<EpicListDto> Epics { get; set; } = Array.Empty<EpicListDto>();
}
