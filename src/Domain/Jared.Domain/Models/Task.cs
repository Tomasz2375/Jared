using Jared.Shared.Abstractions;
using Jared.Shared.Enums;

namespace Jared.Domain.Models;

public class Task : Entity
{
    public int ProjectId { get; set; }
    public int? EpicId { get; set; }
    public int? ParentId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Code { get; set; } = default!;
    public Shared.Enums.TaskStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }
    public TimeSpan EstimatedTime { get; set; }
    public TimeSpan TotalWorkTime { get; set; }

    public Project? Project { get; set; }
    public Epic? Epic { get; set; }
    public Task? Parent { get; set; }

    public List<TaskHistory> TaskHistories { get; set; } = new();
}
