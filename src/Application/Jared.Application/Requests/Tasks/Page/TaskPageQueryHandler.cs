using Jared.Application.Services.Filters;
using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.PageDtos;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Enums;
using Jared.Shared.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Jared.Application.Requests.Tasks.Page;

public class TaskPageQueryHandler : IRequestHandler<TaskPageQuery, Result<TaskPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;
    private readonly IFilter<Domain.Models.Task> filter;

    public TaskPageQueryHandler(
        IDataContext dataContext,
        IMapper mapper,
        IFilterStrategy<Domain.Models.Task> strategy,
        IFilterBuilder<Domain.Models.Task> filterBuilder)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
        filter = filterBuilder.Build(strategy);
    }

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

            tasksQuery = filterResult(tasksQuery, query);
            var pagination = createPagination(tasksQuery, query);
            tasksQuery = sortResult(tasksQuery, query);
            tasksQuery = paginateResult(tasksQuery, query);
            var tasks = await tasksQuery.ToListAsync();

            TaskPageDto result = new()
            {
                Pagination = pagination,
                Tasks = mapper.Map<List<TaskListDto>>(tasks),
            };

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<TaskPageDto>(ex.Message);
        }
    }

    private IQueryable<Domain.Models.Task> filterResult(
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

    private PaginationDto createPagination(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        return new()
        {
            ItemsCount = tasks.Count(),
            ItemFrom = (query.page - 1) * query.pageSize + 1,
            ItemTo = query.page * query.pageSize > tasks.Count() ?
                tasks.Count() :
                query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (tasks.Count() + query.pageSize - 1) / query.pageSize,
        };
    }

    private IQueryable<Domain.Models.Task> sortResult(
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

        return query.SortingDirection == SortingDirection.Descending ?
            tasks.OrderByDescending(sortByExpression) :
            tasks.OrderBy(sortByExpression);
    }

    private IQueryable<Domain.Models.Task> paginateResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        return tasks
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
