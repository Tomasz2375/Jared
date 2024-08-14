using Jared.Shared.Dtos.Abstractions;
using Jared.Shared.Enums;
using TaskStatus = Jared.Shared.Enums.TaskStatus;

namespace Jared.Shared.Dtos.TaskDtos;

public class TaskRootDto : EntityDto<int>
{
    public string Title { get; set; } = default!;
    public int ProjectId { get; set; }
    public string? Code { get; set; }
    public int? EpicId { get; set; }

    public TaskStatus Status { get; set; }
    public Priority Priority { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; }
}
