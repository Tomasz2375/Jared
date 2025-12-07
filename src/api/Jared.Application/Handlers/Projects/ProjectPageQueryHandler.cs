using System.Linq.Expressions;
using Jared.Application.Services.Filters;
using Jared.Contracts.Projects;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Dtos.Projects;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Projects;

public class ProjectPageQueryHandler(
    IDataContext dataContext,
    IMapper mapper,
    IFilterStrategy<Project> strategy,
    IFilterBuilder<Project> filterBuilder)
    : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;
    private readonly IFilter<Project> filter = filterBuilder.Build(strategy);

    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var projectsQuery = dataContext
                .Set<Project>()
                .AsNoTracking();
            var filteredQuery = applyFilters(projectsQuery, query);
            var totalCount = await filteredQuery.CountAsync();
            var sortedQuery = applySorting(filteredQuery, query);
            var paginatedQuery = applyPagination(sortedQuery, query);
            var projects = await paginatedQuery.ToListAsync();
            ProjectPageDto result = new()
            {
                Items = mapper.Map<List<ProjectListDto>>(projects),
                Page = query.page,
                PageSize = query.pageSize,
                TotalItems = totalCount,
                TotalPages = (totalCount + query.pageSize - 1) / query.pageSize,
            };

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<ProjectPageDto>(ex.Message);
        }
    }

    private static IQueryable<Project> applySorting(
        IQueryable<Project> projects,
        ProjectPageQuery query)
    {
        if (query.sortingProperty is null)
        {
            return projects.OrderBy(x => x.Id);
        }

        Dictionary<string, Expression<Func<Project, object>>> columnSelector = new()
        {
            { nameof(ProjectListDto.Id), x => x.Id },
            { nameof(ProjectListDto.Title), x => x.Title },
            { nameof(ProjectListDto.Code), x => x.Code },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.sortingDirection == SortingDirection.Descending ?
            projects.OrderByDescending(sortByExpression) :
            projects.OrderBy(sortByExpression);
    }

    private static IQueryable<Project> applyPagination(
        IQueryable<Project> projects,
        ProjectPageQuery query)
    {
        return projects
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }

    private IQueryable<Project> applyFilters(
        IQueryable<Project> projects,
        ProjectPageQuery query)
    {
        foreach (var (key, value) in query.filters!)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

            projects = filter.ApplyFilters(projects, key, value);
        }

        return projects;
    }
}
