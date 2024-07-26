using Jared.Application.Dtos.PageDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using Jared.Domain.Enums;
using Jared.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

    public async Task<Result<TaskPageDto>> Handle(TaskPageQuery query, CancellationToken cancellationToken)
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
        var tasks = await tasksQuery.ToListAsync();

        TaskPageDto result = new()
        {
            Pagination = pagination,
            Tasks = mapper.Map<List<TaskListDto>>(tasks),
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
        foreach (var (key, value) in query.filters!)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }
            if (key == nameof(TaskListDto.Id))
            {
                tasks = tasks.Where(x => x.Id.ToString().Contains(value!));
            }
            else if (key == nameof(TaskListDto.Title))
            {
                tasks = tasks.Where(x => x.Title.Contains(value!));
            }
            else if (key == nameof(TaskListDto.Code))
            {
                tasks = tasks.Where(x => x.Code.Contains(value!));
            }
            else if (key == nameof(TaskListDto.EpicId))
            {
                tasks = tasks.Where(x => x.Id.ToString().Contains(value!));
            }
            else if (key == nameof(TaskListDto.Status))
            {
                if (value == "0")
                {
                    continue;
                }
                tasks = tasks.Where(x => ((int)x.Status & int.Parse(value!)) != 0);
            }
            else if (key == nameof(TaskListDto.Priority))
            {
                if (value == "0")
                {
                    continue;
                }
                tasks = tasks.Where(x => ((int)x.Priority & int.Parse(value!)) != 0);
            }
            else if (key == nameof(TaskListDto.CreatedAt))
            {
                var hasDateFrom = DateTime.TryParse(value!.Split('-')[0], out DateTime dateFrom);
                var hasDateTo = DateTime.TryParse(value!.Split('-')[1], out DateTime dateTo);

                tasks = tasks.Where(x => !hasDateFrom || x.CreatedAt >= dateFrom);
                tasks = tasks.Where(x => !hasDateTo || x.CreatedAt <= dateTo);
            }
            else if (key == nameof(TaskListDto.Deadline))
            {
                var hasDateFrom = DateTime.TryParse(value!.Split('-')[0], out DateTime dateFrom);
                var hasDateTo = DateTime.TryParse(value!.Split('-')[1], out DateTime dateTo);

                tasks = tasks.Where(x => !hasDateFrom || x.Deadline >= dateFrom);
                tasks = tasks.Where(x => !hasDateTo || x.Deadline <= dateTo);
            }
        }

        return tasks;
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
