using Jared.Application.Dtos.PageDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jared.Application.Queries.ProjectQueries;

public class ProjectListQueryHandler : IRequestHandler<ProjectListQuery, Result<ProjectPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ProjectListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result<ProjectPageDto>> Handle(ProjectListQuery query, CancellationToken cancellationToken)
#pragma warning disable CS1998
    {
        var projectsQuery = dataContext
            .Set<Project>()
            .AsNoTracking();

        projectsQuery = filterResult(projectsQuery, query);

        PaginationDto pagination = new()
        {
            ItemsCount = projectsQuery.Count(),
            ItemFrom = (query.page - 1) * query.pageSize + 1,
            ItemTo = query.page * query.pageSize > projectsQuery.Count() ?
                projectsQuery.Count() :
                query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (projectsQuery.Count() + query.pageSize - 1) / query.pageSize,
        };

        projectsQuery = sortResult(projectsQuery, query);
        projectsQuery = paginateResult(projectsQuery, query);
        var projects = projectsQuery.AsEnumerable();

        ProjectPageDto result = new()
        {
            Pagination = pagination,
            Projects = mapper.Map<IEnumerable<ProjectListDto>>(projects),
        };

        return Result.Ok(result);
    }

    private IQueryable<Project> filterResult(
        IQueryable<Project> projects,
        ProjectListQuery query)
    {
        return projects.Where(x => string.IsNullOrEmpty(query.filter) ||
            x.Title.ToLower().Contains(query.filter.ToLower()) ||
            x.Description!.ToLower().Contains(query.filter.ToLower()));
    }

    private IQueryable<Project> sortResult(
        IQueryable<Project> projects,
        ProjectListQuery query)
    {
        if (query.sortingProperty is null)
        {
            return projects.OrderBy(x => x.Id);
        }

        Dictionary<string, Expression<Func<Project, object>>> columnSelector = new()
        {
            { nameof(ProjectListDto.Id), x => x.Id },
            { nameof(ProjectListDto.Title), x => x.Title },
            { nameof(ProjectListDto.Code), x => x.Code! },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.SortingDirection == SortingDirection.Descending ?
            projects.OrderByDescending(sortByExpression) :
            projects.OrderBy(sortByExpression);
    }

    private IQueryable<Project> paginateResult(
        IQueryable<Project> projects,
        ProjectListQuery query)
    {
        return projects
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
