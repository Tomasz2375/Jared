using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Commands.TaskCommand;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public CreateTaskCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }
#pragma warning disable CS1998
    public async Task<Result> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        var task = mapper.Map<Domain.Models.Task>(command.dto);

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
