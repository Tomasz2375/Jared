using Jared.Application.Dtos.PageDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using Jared.Domain.Models;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jared.Application.Queries.TaskQueries;

public class TaskPageQueryHandler : IRequestHandler<TaskPageQuery, Result<TaskPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskPageQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

#pragma warning disable CS1998
    public async Task<Result<TaskPageDto>> Handle(TaskPageQuery query, CancellationToken cancellationToken)
#pragma warning restore CS1998
    {
        var tasksQuery = dataContext
            .Set<Domain.Models.Task>()
            .Include(x => x.Epic)
            .Include(x => x.Project)
            .AsNoTracking();

        tasksQuery = filterResult(tasksQuery, query);

        var pagination = createPagination(tasksQuery, query);

        tasksQuery = sortResult(tasksQuery, query);
        tasksQuery = paginateResult(tasksQuery, query);
        var tasks = tasksQuery.AsEnumerable();

        TaskPageDto result = new()
        {
            Pagination = pagination,
            Tasks = mapper.Map<IEnumerable<TaskListDto>>(tasks),
        };

        return Result.Ok(result);
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

    private IQueryable<Domain.Models.Task> filterResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskPageQuery query)
    {
        return tasks.Where(x => string.IsNullOrEmpty(query.filter) ||
            (x.Title.ToLower().Contains(query.filter.ToLower()) ||
            x.Description!.ToLower().Contains(query.filter.ToLower())));
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
