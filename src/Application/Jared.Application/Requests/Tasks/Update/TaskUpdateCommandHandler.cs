using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Dtos.TaskHistoryDtos;
using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.Update;

public class TaskUpdateCommandHandler : IRequestHandler<TaskUpdateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly ITaskHistoryService taskHistoryService;
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public TaskUpdateCommandHandler(
        IDataContext dataContext,
        ITaskHistoryService taskHistoryService,
        IMapper mapper,
        IUserService userService)
    {
        this.dataContext = dataContext;
        this.taskHistoryService = taskHistoryService;
        this.mapper = mapper;
        this.userService = userService;
    }

    public async Task<Result> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = userService.GetUser().Id;
            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                    .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == command.dto.Id);

            var changes = taskHistoryService.GetChanged(mapper.Map<TaskDetailsDto>(task), command.dto, userId);
            command.dto.TaskHistories.AddRange(changes);

            command.dto.Adapt(task);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
