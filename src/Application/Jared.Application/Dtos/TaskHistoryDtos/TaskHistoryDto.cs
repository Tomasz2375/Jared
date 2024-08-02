using Jared.Application.Dtos.Abstractions;

namespace Jared.Application.Dtos.TaskHistoryDtos;

public class TaskHistoryDto : EntityDto<int>
{
    public string Property { get; set; } = default!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UserId { get; set; }
}
