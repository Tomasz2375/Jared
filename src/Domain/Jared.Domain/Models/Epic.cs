using Jared.Shared.Abstractions;
using Jared.Shared.Enums;

namespace Jared.Domain.Models;

public class Epic : Entity
{
    public int? ParentId { get; set; }
    public int ProjectId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public Epic? Parent { get; set; }
    public Project? Project { get; set; } = default!;

    public EpicStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Deadline { get; set; }

    public List<Task> Tasks { get; set; } = new();
}
