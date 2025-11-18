using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.Create;

public class TaskCreateCommandHandler(
    IDataContext dataContext,
    IMapper mapper,
    ITaskHistoryService taskHistoryService,
    IUserService userService)
    : IRequestHandler<TaskCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;
    private readonly ITaskHistoryService taskHistoryService = taskHistoryService;
    private readonly IUserService userService = userService;

    public async Task<Result<bool>> Handle(TaskCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = userService.GetUser().Id;
            var project = await dataContext
                .Set<Project>()
                .FirstAsync(x => x.Id == command.dto.ProjectId, cancellationToken);

            project.LastTaskNumber++;
            command.dto.Code = createCode(project);
            var changes = taskHistoryService.GetChanged(new(), command.dto, userId);
            command.dto.TaskHistories.AddRange(changes);
            var task = mapper.Map<Domain.Models.Task>(command.dto);

            dataContext.Add(task);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }

    private static string createCode(Project project)
    {
        return project.Code + " - " + project.LastTaskNumber;
    }
}
