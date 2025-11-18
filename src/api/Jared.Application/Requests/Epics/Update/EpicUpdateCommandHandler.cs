using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Epics.Update;

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
