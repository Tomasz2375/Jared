using Jared.Application.Dtos.TaskDto;
using Mapster;

namespace Jared.Application.Mapping;

public class TaskMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Domain.Models.Task, TaskListDto>()
            .IgnoreNullValues(true);
        config
            .NewConfig<TaskListDto, Domain.Models.Task>()
            .IgnoreNullValues(true);

        config
            .NewConfig<Domain.Models.Task, TaskDetailsDto>()
            .IgnoreNullValues(true);
        config
            .NewConfig<TaskDetailsDto, Domain.Models.Task>()
            .IgnoreNullValues(true);

    }
}
