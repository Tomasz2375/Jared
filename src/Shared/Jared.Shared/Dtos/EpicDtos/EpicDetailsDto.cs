using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Shared.Dtos.EpicDtos;

public class EpicDetailsDto : EpicRootDto
{
    public string? Description { get; set; }
    public IEnumerable<TaskListDto> Tasks { get; set; } = Array.Empty<TaskListDto>();
}
