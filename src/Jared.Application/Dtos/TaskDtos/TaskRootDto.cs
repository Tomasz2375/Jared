using Jared.Application.Dtos.Abstractions;
using Jared.Domain.Enums;
using TaskStatus = Jared.Domain.Enums.TaskStatus;

namespace Jared.Application.Dtos.TaskDtos;

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
