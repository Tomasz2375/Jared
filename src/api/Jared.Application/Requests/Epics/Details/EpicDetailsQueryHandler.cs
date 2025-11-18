using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Epics.Details;

public class EpicDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<EpicDetailsDto>> Handle(EpicDetailsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var epic = await dataContext.Set<Epic>()
                .AsNoTracking()
                .Include(x => x.Project)
                .FirstAsync(x => x.Id == query.id, cancellationToken);

            var result = mapper.Map<EpicDetailsDto>(epic);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<EpicDetailsDto>(ex.Message);
        }
    }
}
