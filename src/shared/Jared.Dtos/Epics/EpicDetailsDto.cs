using Jared.Dtos.Tasks;

namespace Jared.Dtos.Epics;

public class EpicDetailsDto : EpicRootDto
{
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }

    public IEnumerable<TaskListDto> Tasks { get; set; } = Array.Empty<TaskListDto>();
}
