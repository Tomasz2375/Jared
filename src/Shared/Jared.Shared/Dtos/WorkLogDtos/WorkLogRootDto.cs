using Jared.Shared.Dtos.Abstractions;

namespace Jared.Shared.Dtos.WorkLogDtos;

public class WorkLogRootDto : EntityDto<int>
{
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public TimeSpan Time { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime LoggedDate { get; set; } = DateTime.Now;
}
