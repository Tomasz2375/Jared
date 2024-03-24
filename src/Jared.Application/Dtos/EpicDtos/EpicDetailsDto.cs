using Jared.Application.Dtos.TaskDtos;

namespace Jared.Application.Dtos.EpicDtos;

public class EpicDetailsDto : EpicRootDto
{
    public string? Description { get; set; }
    public IEnumerable<TaskListDto> Tasks { get; set; } = Array.Empty<TaskListDto>();
}
