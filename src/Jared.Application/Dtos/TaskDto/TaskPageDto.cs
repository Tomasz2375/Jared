using Jared.Application.Dtos.PageDto;

namespace Jared.Application.Dtos.TaskDto;

public class TaskPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public IEnumerable<TaskListDto> Tasks { get; set; } = Array.Empty<TaskListDto>();
}
