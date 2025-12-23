using Jared.Application.Abstractions;
using Jared.Application.Services.TaskHistory;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Tasks;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Tasks;

public class TaskUpdateCommandHandler(
    IDataContext dataContext,
    ITaskHistoryService taskHistoryService,
    IMapper mapper,
    IUserService userService)
    : IRequestHandler<TaskUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly ITaskHistoryService taskHistoryService = taskHistoryService;
    private readonly IMapper mapper = mapper;
    private readonly IUserService userService = userService;

    public async Task<Result<bool>> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = userService.GetUser().Id;
            command.dto.WorkLogs = command.dto.WorkLogs.Where(x => x.UserId > 0 || !x.Delete).ToList();

            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                    .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == command.dto.Id, cancellationToken);

            var changes = taskHistoryService.GetChanged(mapper.Map<TaskDetailsDto>(task), command.dto, userId);
            command.dto.TaskHistories.AddRange(changes);

            command.dto.Adapt(task);

            var deletedWorkLogsIds = command.dto.WorkLogs.Where(x => x.Delete).Select(x => x.UserId);
            task.WorkLogs.RemoveAll(x => deletedWorkLogsIds.Contains(x.Id));

            var deletedWorkLogs = await dataContext
                .Set<WorkLog>()
                .Where(x => deletedWorkLogsIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            dataContext.RemoveRange(deletedWorkLogs);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
