using Jared.Contracts.Epics;
using Jared.Core.Abstractions;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Epics;

public class EpicUpdateCommandHandler(IDataContext dataContext)
    : IRequestHandler<EpicUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext = dataContext;

    public async Task<Result<bool>> Handle(EpicUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var epic = await dataContext.Set<Epic>()
                .FirstAsync(x => x.Id == command.dto.Id, cancellationToken);

            command.dto.Adapt(epic);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>(ex.Message);
        }
    }
}
