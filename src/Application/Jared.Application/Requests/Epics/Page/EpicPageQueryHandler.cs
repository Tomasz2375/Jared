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

namespace Jared.Application.Requests.Epics.Page;

public class EpicPageQueryHandler : IRequestHandler<EpicPageQuery, Result<EpicPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public EpicPageQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<EpicPageDto>> Handle(EpicPageQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var epicsQuery = dataContext
                .Set<Epic>()
                .Include(x => x.Project)
                .AsNoTracking();

            epicsQuery = filterResult(epicsQuery, query);
            var pagination = createPagination(epicsQuery, query);

            epicsQuery = sortResult(epicsQuery, query);
            epicsQuery = paginateResult(epicsQuery, query);
            var epics = await epicsQuery.ToListAsync();

            EpicPageDto result = new()
            {
                Pagination = pagination,
                Epics = mapper.Map<List<EpicListDto>>(epics),
            };

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<EpicPageDto>(ex.Message);
        }
    }


    private IQueryable<Epic> filterResult(
        IQueryable<Epic> epics,
        EpicPageQuery query)
    {
        foreach (var (key, value) in query.filter!)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }
            if (key == nameof(EpicListDto.Id))
            {
                epics = epics.Where(x => x.Id.ToString().Contains(value!));
            }
            else if (key == nameof(EpicListDto.Title))
            {
                epics = epics.Where(x => x.Title.Contains(value!));
            }
            else if (key == nameof(EpicListDto.ParentId))
            {
                epics = epics.Where(x => x.ParentId.ToString()!.Contains(value));
            }
            else if (key == nameof(EpicListDto.ProjectId))
            {
                epics = epics.Where(x => x.ProjectId.ToString().Contains(value));
            }
        }

        return epics;
    }

    private PaginationDto createPagination(
        IQueryable<Epic> epics,
        EpicPageQuery query)
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
        EpicPageQuery query)
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
        EpicPageQuery query)
    {
        return epics
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
