using Jared.Application.Dtos.EpicDtos;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Requests.Epics.Details;

public class EpicDetailsQueryHandler : IRequestHandler<EpicDetailsQuery, Result<EpicDetailsDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

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
