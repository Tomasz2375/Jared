namespace Jared.Application.Dtos.TaskDto
{
    public class TaskRootDto : EntityDto<int>
    {
        public string Title { get; set; } = default!;
        public string? Code { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
    }
}
