using Jared.Application.Dtos.Abstractions;
using Jared.Domain.Enums;
using TaskStatus = Jared.Domain.Enums.TaskStatus;

namespace Jared.Application.Dtos.TaskDto
{
    public class TaskRootDto : EntityDto<int>
    {
        public string Title { get; set; } = default!;
        public string? Code { get; set; }
        public TaskStatus Status { get; set; }
        public Priority Priority { get; set; }
    }
}
