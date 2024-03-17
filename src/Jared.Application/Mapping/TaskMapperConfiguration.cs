using Jared.Application.Dtos.TaskDto;
using Mapster;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application.Mapping;

public class TaskMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaskRootDto, Task>()
            .Map(
                d => d.PriorityId,
                s => s.Priority)
            .Map(
                d => d.StatusId,
                s => s.Status);

        config.NewConfig<Task, TaskRootDto>()
            .Map(
                d => d.Status,
                s => s.StatusId)
            .Map(
                d => d.Priority,
                s => s.PriorityId);

        config.NewConfig<Task, TaskListDto>()
            .Inherits<Task, TaskRootDto>();

        config.NewConfig<TaskListDto, Task>()
            .Inherits<TaskRootDto, Task>();

        config.NewConfig<Task, TaskDetailsDto>()
            .Inherits<Task, TaskRootDto>();

        config.NewConfig<TaskDetailsDto, Task>()
            .Inherits<TaskRootDto, Task>();
    }
}
