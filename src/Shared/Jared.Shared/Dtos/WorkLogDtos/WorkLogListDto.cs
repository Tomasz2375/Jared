namespace Jared.Shared.Dtos.WorkLogDtos;

public class WorkLogListDto : WorkLogRootDto
{
    public string UserFullName { get; set; } = default!;
    public bool Delete { get; set; }
}
