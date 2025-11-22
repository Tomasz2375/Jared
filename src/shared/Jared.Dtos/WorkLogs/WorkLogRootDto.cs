using Jared.Shared.Dtos.Abstractions;

namespace Jared.Dtos.WorkLogs;

public class WorkLogRootDto : BaseDto
{
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public TimeSpan Time { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime LoggedDate { get; set; } = DateTime.Now;
}
