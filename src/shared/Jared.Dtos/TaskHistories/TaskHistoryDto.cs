namespace Jared.Dtos.TaskHistories;

public class TaskHistoryDto : BaseDto
{
    public string Property { get; set; } = default!;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UserId { get; set; }
}
