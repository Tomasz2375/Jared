using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Requests.Epics.Create;

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
