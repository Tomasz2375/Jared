using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.PageDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jared.Application.Queries.EpicQueries;

public class EpicListQueryHandler : IRequestHandler<EpicListQuery, Result<EpicPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result<EpicPageDto>> Handle(EpicListQuery query, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        var epicsQuery = dataContext
            .Set<Epic>()
            .Include(x => x.Project)
            .AsNoTracking();

        epicsQuery = filterResult(epicsQuery, query);
        var pagination = createPagination(epicsQuery, query);

        epicsQuery = sortResult(epicsQuery, query);
        epicsQuery = paginateResult(epicsQuery, query);
        var epics = epicsQuery.AsEnumerable();

        EpicPageDto result = new()
        {
            Pagination = pagination,
            Epics = mapper.Map<IEnumerable<EpicListDto>>(epics),
        };

        return Result.Ok(result);
    }


    private IQueryable<Epic> filterResult(
        IQueryable<Epic> epics,
        EpicListQuery query)
    {
        return epics.Where(x => string.IsNullOrEmpty(query.filter) ||
            x.Title.ToLower().Contains(query.filter.ToLower()) ||
            x.Description!.ToLower().Contains(query.filter.ToLower()));
    }

    private PaginationDto createPagination(
        IQueryable<Epic> epics,
        EpicListQuery query)
    {
        return new()
        {
            ItemsCount = epics.Count(),
            ItemFrom = (query.page - 1) * query.pageSize + 1,
            ItemTo = query.page * query.pageSize > epics.Count() ?
                epics.Count() :
                query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (epics.Count() + query.pageSize - 1) / query.pageSize,
        };
    }

    private IQueryable<Epic> sortResult(
        IQueryable<Epic> epics,
        EpicListQuery query)
    {
        if (query.sortingProperty is null)
        {
            return epics.OrderBy(x => x.Id);
        }

        Dictionary<string, Expression<Func<Epic, object>>> columnSelector = new()
        {
            { nameof(EpicListDto.Id), x => x.Id },
            { nameof(EpicListDto.Title), x => x.Title },
            { nameof(EpicListDto.ParentId), x => x.ParentId! },
            { nameof(EpicListDto.ProjectId), x => x.ProjectId },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.SortingDirection == SortingDirection.Descending ?
            epics.OrderByDescending(sortByExpression) :
            epics.OrderBy(sortByExpression);
    }

    private IQueryable<Epic> paginateResult(
        IQueryable<Epic> epics,
        EpicListQuery query)
    {
        return epics
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
