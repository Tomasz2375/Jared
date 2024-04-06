using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Commands.TaskCommands;

public class TaskUpdateCommandHandler : IRequestHandler<TaskUpdateCommand, Result>
{
    private readonly IDataContext dataContext;

    public TaskUpdateCommandHandler(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Result> Handle(TaskUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await dataContext.Set<Domain.Models.Task>()
                .Include(x => x.Project)
                .Include(x => x.Epic)
                .FirstAsync(x => x.Id == command.dto.Id);

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
