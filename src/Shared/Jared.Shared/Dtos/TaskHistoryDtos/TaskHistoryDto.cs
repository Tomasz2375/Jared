using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.TaskHistoryDtos;

public class TaskHistoryDto : EntityDto<int>
{
    public string Property { get; set; } = default!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UserId { get; set; }
}
