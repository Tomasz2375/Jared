namespace Jared.Domain.Models;

public class Task
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int? ProjectId { get; set; }
    public int? EpicId { get; set; }
    public int StatusId { get; set; }
    public int PriorityId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Code { get; set; } = default!;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }
    public TimeSpan EstimatedTime { get; set; }
    public TimeSpan TotalWorkTime { get; set; }

    public Task? Parent { get; set; }
    public Project? Project { get; set; }
    public Epic? Epic { get; set; }


}
