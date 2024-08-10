using Jared.Application.Services.TaskHistory;
using Jared.Application.Services.User;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Tasks.Create;

public class TaskCreateCommandHandler : IRequestHandler<TaskCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly ITaskHistoryService taskHistoryService;
    private readonly IUserService userService;

    public TaskCreateCommandHandler(
        IDataContext dataContext,
        IMapper mapper,
        ITaskHistoryService taskHistoryService,
        IUserService userService)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.taskHistoryService = taskHistoryService;
        this.userService = userService;
    }

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

    private string createCode(Project project)
    {
        return project.Code + " - " + project.LastTaskNumber;
    }
}
