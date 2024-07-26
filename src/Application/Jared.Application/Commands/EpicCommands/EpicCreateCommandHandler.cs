using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Commands.EpicCommands;

public class EpicCreateCommandHandler : IRequestHandler<EpicCreateCommand, Result>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result> Handle(EpicCreateCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        var epic = mapper.Map<Epic>(command.dto);

        try
        {
            dataContext.Add(epic);
            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
