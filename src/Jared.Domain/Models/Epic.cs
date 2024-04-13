using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class Epic : Entity
{
    public int? ParentId { get; set; }
    public int ProjectId { get; set; }

    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public Epic? Parent { get; set; }
    public Project Project { get; set; } = default!;

    public List<Task> Tasks { get; set; } = new();
}
