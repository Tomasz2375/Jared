using Jared.Domain.Abstractions;
using Jared.Shared.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;

namespace Jared.Application.Requests.Epics.Create;

public class EpicCreateCommandHandler : IRequestHandler<EpicCreateCommand, Result<bool>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicCreateCommandHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

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
