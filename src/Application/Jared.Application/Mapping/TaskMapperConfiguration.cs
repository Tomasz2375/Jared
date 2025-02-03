using Jared.Shared.Dtos.TaskDtos;
using Mapster;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application.Mapping;

public class TaskMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Task, TaskDetailsDto>()
            .Map(d => d.TotalWorkTime, s => new TimeSpan(s.WorkLogs.Sum(x => x.Time.Ticks)));
    }
}
