namespace Jared.Shared.Dtos.WorkLogDtos;

public class WorkLogAddDto
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public DateTime? WorkDate { get; set; } = DateTime.Now.Date;
}
