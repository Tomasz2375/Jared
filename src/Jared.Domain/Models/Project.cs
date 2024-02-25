using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class Project : Entity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public string Code { get; set; } = default!;

    public List<Task> Tasks { get; set; } = new();
    public List<Epic> Epics { get; set; } = new();
}
