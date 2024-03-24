using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Queries.EpicQueries;

internal class EpicListQueryHandler : IRequestHandler<EpicListQuery, Result<List<EpicListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result<List<EpicListDto>>> Handle(EpicListQuery query, CancellationToken cancellationToken)
#pragma warning disable CS1998
    {
        var epicsQuery = dataContext
            .Set<Epic>()
            .AsNoTracking();

        epicsQuery = getByProjectId(epicsQuery, query);

        var epics = epicsQuery.AsEnumerable();

        var result = mapper.Map<List<EpicListDto>>(epics);

        return Result.Ok(result);
    }

    private IQueryable<Epic> getByProjectId(IQueryable<Epic> epics, EpicListQuery query)
    {
        return epics.Where(x => query.projectId == null || x.Id == query.projectId);
    }
}
