using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Shared.Dtos.EpicDtos;

public class EpicDetailsDto : EpicRootDto
{
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }

    public IEnumerable<TaskListDto> Tasks { get; set; } = Array.Empty<TaskListDto>();
}
