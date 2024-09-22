using Jared.Application.Services.Filters;
using Jared.Domain.Models;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.PageDtos;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Enums;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jared.Application.Requests.Projects.Page;

public class ProjectPageQueryHandler : IRequestHandler<ProjectPageQuery, Result<ProjectPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IFilter<Project> filter;

    public ProjectPageQueryHandler(
        IDataContext dataContext,
        IMapper mapper,
        IFilterStrategy<Project> strategy,
        IFilterBuilder<Project> filterBuilder)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        filter = filterBuilder.Build(strategy);
    }

    public async Task<Result<ProjectPageDto>> Handle(ProjectPageQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var projectsQuery = dataContext
                .Set<Project>()
                .AsNoTracking();

            projectsQuery = filterResult(projectsQuery, query);
            var pagination = createPagination(projectsQuery, query);
            projectsQuery = sortResult(projectsQuery, query);
            projectsQuery = paginateResult(projectsQuery, query);
            var projects = await projectsQuery.ToListAsync();

            ProjectPageDto result = new()
            {
                Pagination = pagination,
                Projects = mapper.Map<List<ProjectListDto>>(projects),
            };

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<ProjectPageDto>(ex.Message);
        }
    }

    private IQueryable<Project> filterResult(
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

    private PaginationDto createPagination(
        IQueryable<Project> projects,
        ProjectPageQuery query)
    {
        return new()
        {
            ItemsCount = projects.Count(),
            ItemFrom = (query.page - 1) * query.pageSize + 1,
            ItemTo = query.page * query.pageSize > projects.Count() ?
                projects.Count() :
                query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (projects.Count() + query.pageSize - 1) / query.pageSize,
        };
    }

    private IQueryable<Project> sortResult(
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

        return query.SortingDirection == SortingDirection.Descending ?
            projects.OrderByDescending(sortByExpression) :
            projects.OrderBy(sortByExpression);
    }

    private IQueryable<Project> paginateResult(
        IQueryable<Project> projects,
        ProjectPageQuery query)
    {
        return projects
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
