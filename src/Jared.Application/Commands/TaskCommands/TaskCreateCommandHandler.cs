using Jared.Application.Services.TaskHistory;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Commands.TaskCommand;

public class TaskCreateCommandHandler : IRequestHandler<TaskCreateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly ITaskHistoryService taskHistoryService;

    public TaskCreateCommandHandler(
        IDataContext dataContext,
        IMapper mapper,
        ITaskHistoryService taskHistoryService)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        this.taskHistoryService = taskHistoryService;
    }

    public async Task<Result> Handle(TaskCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var project = await dataContext
                .Set<Project>()
                .FirstAsync(x => x.Id == command.dto.ProjectId, cancellationToken);

            project.LastTaskNumber++;
            command.dto.Code = createCode(project);
            var changes = taskHistoryService.GetChanged(new(), command.dto);
            command.dto.TaskHistories.AddRange(changes);
            var task = mapper.Map<Domain.Models.Task>(command.dto);

            dataContext.Add(task);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }

    private string createCode(Project project)
    {
        return project.Code + " - " + project.LastTaskNumber;
    }
}
