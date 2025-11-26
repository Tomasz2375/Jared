using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Handlers.Epics;

public class EpicCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<EpicCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<bool>> Handle(EpicCreateCommand command, CancellationToken cancellationToken)
    {
        var epic = mapper.Map<Epic>(command.dto);

        try
        {
            dataContext.Add(epic);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
