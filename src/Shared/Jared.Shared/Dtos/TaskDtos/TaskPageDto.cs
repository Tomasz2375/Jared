using Jared.Shared.Dtos.PageDtos;

namespace Jared.Shared.Dtos.TaskDtos;

public class TaskPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<TaskListDto> Tasks { get; set; } = new();
}
