using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Jared.Application.Queries.TaskQueries;

public class TaskListQueryHandler : IRequestHandler<TaskListQuery, Result<TaskPageDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<TaskPageDto>> Handle(TaskListQuery query, CancellationToken cancellationToken)
    {
        var tasks = dataContext
            .Set<Domain.Models.Task>()
            .AsNoTracking();

        tasks = filterResult(tasks, query);
        TaskPageDto result = new()
        {
            TasksCount = tasks.Count(),
            TasksFrom = (query.page - 1) * query.pageSize + 1,
            TasksTo = query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (tasks.Count() + query.pageSize - 1) / query.pageSize,
        };
        tasks = sortResult(tasks, query);
        tasks = paginateResult(tasks, query);
        result.Tasks = mapper.Map<List<TaskListDto>>(tasks);

        return Result.Ok(result);
    }


    private IQueryable<Domain.Models.Task> filterResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskListQuery query)
    {
        return tasks.Where(x => query.filter == null ||
            (x.Title.ToLower().Contains(query.filter.ToLower()) ||
            x.Description!.ToLower().Contains(query.filter.ToLower())));
    }
    private IQueryable<Domain.Models.Task> sortResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskListQuery query)
    {
        if (query.sortingProperty is null || query.SortingDirection is null)
        {
            return tasks;
        }

        Dictionary<string, Expression<Func<Domain.Models.Task, object>>> columnSelector = new()
        {
            { nameof(Domain.Models.Task.Id), x => x.Id },
            { nameof(Domain.Models.Task.Title), x => x.Title },
            { nameof(Domain.Models.Task.StartDate), x => x.StartDate! },
            { nameof(Domain.Models.Task.EndDate), x => x.EndDate! },
            { nameof(Domain.Models.Task.StatusId), x => x.StatusId },
            { nameof(Domain.Models.Task.PriorityId), x => x.PriorityId },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.SortingDirection == SortingDirection.Ascending ?
            tasks.OrderBy(sortByExpression) :
            tasks.OrderByDescending(sortByExpression);
    }

    private IQueryable<Domain.Models.Task> paginateResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskListQuery query)
    {
        return tasks
            .Skip((query.page - 1) * query.pageSize)
            .Take(query.pageSize);
    }
}
