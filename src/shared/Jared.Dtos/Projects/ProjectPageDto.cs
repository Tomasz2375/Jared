using Jared.Dtos.Tasks;

namespace Jared.Dtos.Projects;

public class ProjectPageDto : IPagination<ProjectListDto>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<ProjectListDto> Items { get; set; } = new();
}
