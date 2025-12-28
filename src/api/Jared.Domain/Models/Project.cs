using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class Project : Entity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Code { get; set; } = default!;
    public string Color { get; set; } = default!;

    public int LastTaskNumber { get; set; }

    public List<Task> Tasks { get; set; } = new();
}
