using System.Data;
using System.Linq.Expressions;
using Jared.Application.Services.Filters;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Core.Enums;
using Jared.Domain.Abstractions;
using Jared.Dtos.Tasks;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jared.Application.Handlers.Tasks;

public class TaskPageQueryHandler(
    IDataContext dataContext,
    IMapper mapper,
    IFilterStrategy<Domain.Models.Task> strategy,
    IFilterBuilder<Domain.Models.Task> filterBuilder)
    : IRequestHandler<TaskPageQuery, Result<TaskPageDto>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;
    private readonly IFilter<Domain.Models.Task> filter = filterBuilder.Build(strategy);

    public async Task<Result<TaskPageDto>> Handle(TaskPageQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var tasksQuery = dataContext
                .Set<Domain.Models.Task>()
                .Include(x => x.Epic)
                .Include(x => x.Project)
                .Include(x => x.CreatedBy)
                .Include(x => x.AssignedTo)
                .AsNoTracking();
            var filteredQuery = applyFilters(tasksQuery, query);
            var totalCount = await filteredQuery.CountAsync();
            var sortedQuery = applySorting(filteredQuery, query);
            var paginatedQuery = applyPagination(sortedQuery, query);
            var tasks = await paginatedQuery.ToListAsync();
            TaskPageDto result = new()
            {
                Items = mapper.Map<List<TaskListDto>>(tasks),
                Page = query.page,
                PageSize = query.pageSize,
                TotalItems = totalCount,
                TotalPages = (totalCount + query.pageSize - 1) / query.pageSize,
            };

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<TaskPageDto>(ex.Message);
        }
    }

    private static IQueryable<Domain.Models.Task> applySorting(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        if (query.sortingProperty is null)
        {
            return tasks.OrderBy(x => x.Id);
        }

        Dictionary<string, Expression<Func<Domain.Models.Task, object>>> columnSelector = new()
        {
            { nameof(TaskListDto.Id), x => x.Id },
            { nameof(TaskListDto.Title), x => x.Title },
            { nameof(TaskListDto.Code), x => x.Code! },
            { nameof(TaskListDto.Status), x => x.Status },
            { nameof(TaskListDto.Priority), x => x.Priority },
            { nameof(TaskListDto.CreatedAt), x => x.CreatedAt },
            { nameof(TaskListDto.Deadline), x => x.Deadline! },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.sortingDirection == SortingDirection.Descending ?
            tasks.OrderByDescending(sortByExpression) :
            tasks.OrderBy(sortByExpression);
    }

    private static IQueryable<Domain.Models.Task> applyPagination(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        return tasks
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }

    private IQueryable<Domain.Models.Task> applyFilters(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        foreach (var (key, value) in query.filters!)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

            tasks = filter.ApplyFilters(tasks, key, value);
        }

        return tasks;
    }
}
