using Jared.Core.Enums;
using TaskStatus = Jared.Core.Enums.TaskStatus;

namespace Jared.Dtos.Tasks;

public class TaskRootDto : BaseDto
{
    public string Title { get; set; } = default!;
    public int ProjectId { get; set; }
    public string? Code { get; set; }
    public int? EpicId { get; set; }
    public int? CreatedById { get; set; }
    public int? AssignedToId { get; set; }

    public TaskStatus Status { get; set; }
    public Priority Priority { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? Deadline { get; set; }
}
