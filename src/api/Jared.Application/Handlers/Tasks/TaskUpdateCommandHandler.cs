using Jared.Application.Abstractions;
using Jared.Application.Services.TaskHistory;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
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
    : IRequestHandler<TaskUpdateCommand, Result<TaskDetailsDto>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly ITaskHistoryService taskHistoryService = taskHistoryService;
    private readonly IMapper mapper = mapper;
    private readonly IUserService userService = userService;

    public async Task<Result<TaskDetailsDto>> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = userService.GetUser().Id;
            command.dto.WorkLogs = command.dto.WorkLogs.Where(x => !x.Delete).ToList();
            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.WorkLogs)
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .Include(x => x.TaskHistories)
                    .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == command.dto.Id, cancellationToken);

            var changes = taskHistoryService.GetChanged(mapper.Map<TaskDetailsDto>(task), command.dto, userId);
            command.dto.TaskHistories.AddRange(changes);
            command.dto.Adapt(task);

            await dataContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<TaskDetailsDto>(task);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<TaskDetailsDto>(ex.Message);
        }
    }
}
