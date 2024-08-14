using Jared.Domain.Abstractions;
using Jared.Shared.Interfaces;
using Jared.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Epics.Update;

public class EpicUpdateCommandHandler : IRequestHandler<EpicUpdateCommand, Result<bool>>
{
    private readonly IDataContext dataContext;

    public EpicUpdateCommandHandler(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

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
