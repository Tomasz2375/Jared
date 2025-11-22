using Jared.Contracts.Epics;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Epics;
using Jared.Shared.Abstractions;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Epics;

public class EpicListQueryHandler(IDataContext dataContext, IMapper mapper)
    : IRequestHandler<EpicListQuery, Result<List<EpicListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<EpicListDto>>> Handle(EpicListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var epicsQuery = dataContext
                .Set<Epic>()
                .Where(x => query.projectId == null || query.projectId == 0 || x.ProjectId == query.projectId)
                .AsNoTracking();

            var epics = await epicsQuery.ToListAsync();

            var result = mapper.Map<List<EpicListDto>>(epics);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<EpicListDto>>(ex.Message);
        }
    }
}
