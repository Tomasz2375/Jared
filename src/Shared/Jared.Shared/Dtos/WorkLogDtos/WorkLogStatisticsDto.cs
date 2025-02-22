namespace Jared.Shared.Dtos.WorkLogDtos;

public class WorkLogStatisticsDto
{
    public int TaskId { get; set; }
    public string TaskCode { get; set; } = default!;
    public string TaskTitle { get; set; } = default!;
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; } = default!;
    public string Color { get; set; } = default!;
    public TimeSpan Time { get; set; }
    public DateTime WorkDate { get; set; }
}
