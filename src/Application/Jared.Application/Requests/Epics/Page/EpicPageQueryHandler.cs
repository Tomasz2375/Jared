﻿using Jared.Application.Services.Filters;
using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.PageDtos;
using Jared.Shared.Enums;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jared.Application.Requests.Epics.Page;

public class EpicPageQueryHandler : IRequestHandler<EpicPageQuery, Result<EpicPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IFilter<Epic> filter;

    public EpicPageQueryHandler(
        IDataContext dataContext,
        IMapper mapper,
        IFilterStrategy<Epic> strategy,
        IFilterBuilder<Epic> filterBuilder)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        filter = filterBuilder.Build(strategy);
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

            epics = filter.ApplyFilters(epics, key, value);
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
            { nameof(EpicListDto.Status), x => x.Status },
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
