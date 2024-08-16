using Jared.Shared.Dtos.TaskHistoryDtos;

namespace Jared.Shared.Dtos.TaskDtos;

public class TaskDetailsDto : TaskRootDto
{
    public int? ParentId { get; set; }

    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan EstimatedTime { get; set; }
    public TimeSpan TotalWorkTime { get; set; }

    public List<TaskHistoryDto> TaskHistories { get; set; } = new();
}
