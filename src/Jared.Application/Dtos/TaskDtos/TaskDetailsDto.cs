namespace Jared.Application.Dtos.TaskDtos;

public class TaskDetailsDto : TaskRootDto
{
    public int? ParentId { get; set; }
    public int? ProjectId { get; set; }
    public int? EpicId { get; set; }

    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }
}
