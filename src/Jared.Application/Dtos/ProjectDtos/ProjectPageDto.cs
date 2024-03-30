using Jared.Application.Dtos.PageDtos;

namespace Jared.Application.Dtos.ProjectDtos;

public class ProjectPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public IEnumerable<ProjectListDto> Projects { get; set; } = Array.Empty<ProjectListDto>();
}
