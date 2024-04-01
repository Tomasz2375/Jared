using Jared.Application.Dtos.PageDtos;

namespace Jared.Application.Dtos.TaskDtos;

public class TaskPageDto
{
    public PaginationDto Pagination { get; set; } = default!;
    public List<TaskListDto> Tasks { get; set; } = new();
}
