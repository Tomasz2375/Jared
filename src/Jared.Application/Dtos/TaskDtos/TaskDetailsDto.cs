namespace Jared.Application.Dtos.TaskDtos;

public class TaskDetailsDto : TaskRootDto
{
    public int? ParentId { get; set; }

    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }

    public TimeSpan EstimatedTime { get; set; }
    public TimeSpan TotalWorkTime { get; set; }
}
