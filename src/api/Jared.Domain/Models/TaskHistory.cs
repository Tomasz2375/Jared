using Jared.Domain.Abstractions;

namespace Jared.Domain.Models;

public class TaskHistory : Entity
{
    public int TaskId { get; set; }
    public int? UserId { get; set; }

    public string Property { get; set; } = default!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }

    public Task? Task { get; set; }
    public User? User { get; set; }
}
