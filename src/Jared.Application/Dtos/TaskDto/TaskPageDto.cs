namespace Jared.Application.Dtos.TaskDto;

public class TaskPageDto
{
    public int TasksCount { get; set; }
    public int TasksFrom { get; set; }
    public int TasksTo { get; set; }
    public int? CurrentPage { get; set; }
    public int? PageSize { get; set; }
    public int? PageCount { get; set; }
    public List<TaskListDto> Tasks { get; set; } = new();
}
