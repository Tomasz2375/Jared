using Jared.Shared.Dtos.PageDtos;

namespace Jared.Shared.Dtos.ProjectDtos;

public class ProjectPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<ProjectListDto> Projects { get; set; } = new();
}
