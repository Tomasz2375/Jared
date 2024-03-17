using Jared.Application.Dtos.PageDto;
using Jared.Application.Dtos.TaskDto;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        var tasksQuery = dataContext
            .Set<Domain.Models.Task>()
            .Include(x => x.Epic)
            .Include(x => x.Project)
            .AsNoTracking();

        tasksQuery = filterResult(tasksQuery, query);

        PaginationDto pagination = new()
        {
            ItemsCount = tasksQuery.Count(),
            ItemFrom = (query.page - 1) * query.pageSize + 1,
            ItemTo = query.page * query.pageSize > tasksQuery.Count() ?
                tasksQuery.Count() :
                query.page * query.pageSize,
            CurrentPage = query.page,
            PageSize = query.pageSize,
            PageCount = (tasksQuery.Count() + query.pageSize - 1) / query.pageSize,
        };

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


    private IQueryable<Domain.Models.Task> filterResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskListQuery query)
    {
        return tasks.Where(x => string.IsNullOrEmpty(query.filter) ||
            (x.Title.ToLower().Contains(query.filter.ToLower()) ||
            x.Description!.ToLower().Contains(query.filter.ToLower())));
    }

    private IQueryable<Domain.Models.Task> sortResult(
        IQueryable<Domain.Models.Task> tasks,
        TaskListQuery query)
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
            { nameof(TaskListDto.Status), x => x.StatusId },
            { nameof(TaskListDto.Priority), x => x.PriorityId },
        };

        var sortByExpression = columnSelector[query.sortingProperty];

        return query.SortingDirection == SortingDirection.Descending ?
            tasks.OrderByDescending(sortByExpression) :
            tasks.OrderBy(sortByExpression);
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
