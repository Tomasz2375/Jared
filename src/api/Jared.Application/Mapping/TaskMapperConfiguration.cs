using Jared.Dtos.Tasks;
using Mapster;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application.Mapping;

public class TaskMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Task, TaskDetailsDto>()
            .Map(d => d.TotalWorkTime, s => TimeSpan.FromMinutes(s.TotalWorkTimeMinutes))
            .Map(d => d.EstimatedTime, s => TimeSpan.FromMinutes(s.EstimatedTimeMinutes));

        config
            .NewConfig<TaskDetailsDto, Task>()
            .Map(d => d.TotalWorkTimeMinutes, s => (int)s.TotalWorkTime.TotalMinutes)
            .Map(d => d.EstimatedTimeMinutes, s => (int)s.EstimatedTime.TotalMinutes);
    }
}
