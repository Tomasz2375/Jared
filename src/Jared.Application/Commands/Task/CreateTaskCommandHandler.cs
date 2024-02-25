using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using MediatR;
using TaskE = Jared.Domain.Models.Task;

namespace Jared.Application.Commands.Task;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
{
    private readonly IDataContext dataContext;

    public CreateTaskCommandHandler(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Result> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        TaskE task = new()
        {
            Title = command.dto.Title,
            Description = command.dto.Description,
            Deadline = command.dto.Deadline,
            StartDate = command.dto.StartDate,
            EndDate = command.dto.EndDate,
        };

        try
        {
            dataContext.Add(task);
            await dataContext.SaveChangesAsync(cancellationToken);
            
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }
}
