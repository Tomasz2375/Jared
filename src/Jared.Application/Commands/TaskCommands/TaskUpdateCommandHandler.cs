using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Services.TaskHistory;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Commands.TaskCommands;

public class TaskUpdateCommandHandler : IRequestHandler<TaskUpdateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly ITaskHistoryService taskHistoryService;
    private readonly IMapper mapper;

    public TaskUpdateCommandHandler(
        IDataContext dataContext,
        ITaskHistoryService taskHistoryService,
        IMapper mapper)
    {
        this.dataContext = dataContext;
        this.taskHistoryService = taskHistoryService;
        this.mapper = mapper;
    }

    public async Task<Result> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                .FirstAsync(x => x.Id == command.dto.Id);

            var changes = taskHistoryService.GetChanged(mapper.Map<TaskDetailsDto>(task), command.dto);
            command.dto.TaskHistories.AddRange(changes);

            command.dto.Adapt(task);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(command.dto);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
