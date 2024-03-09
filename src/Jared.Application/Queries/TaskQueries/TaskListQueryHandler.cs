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

public class TaskListQueryHandler : IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public TaskListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<List<TaskListDto>>> Handle(TaskListQuery query, CancellationToken cancellationToken)
    {
        var tasks = dataContext
            .Set<Domain.Models.Task>()
            .AsNoTracking();
        
        tasks = filterResult(tasks, query);
        tasks = sortResult(tasks, query);
        tasks = paginateResult(tasks, query);

        var result = mapper.Map<List<TaskListDto>>(tasks);

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
        if (query.sortingProperty is null)
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
        if (query.page is null || query.pageSize is null)
        {
            return tasks;
        }

        return tasks
            .Skip((int)(query.page - 1) * (int)query.pageSize)
            .Take((int)query.pageSize);
    }
}
