using Jared.Shared.Dtos.TaskDtos;
using Task = Jared.Domain.Models.Task;

namespace Jared.Application.Services.Filters;

public class TaskFilter : IFilterStrategy<Task>
{
    public IQueryable<Task> ApplyFilter(IQueryable<Task> query, string key, string value)
    {
        if (filters.TryGetValue(key, out var filter))
        {
            query = filter(query, value);
        }

        return query;
    }

    private Dictionary<string, Func<IQueryable<Task>, string, IQueryable<Task>>> filters = new()
    {
        {
            nameof(TaskListDto.Id),
            (tasks, value) => tasks.Where(x => x.Id.ToString().Contains(value))
        },
        {
            nameof(TaskListDto.Title),
            (tasks, value) => tasks.Where(x => x.Title.Contains(value))
        },
        {
            nameof(TaskListDto.Code),
            (tasks, value) => tasks.Where(x => x.Code.Contains(value))
        },
        {
            nameof(TaskListDto.EpicId),
            (tasks, value) => tasks.Where(x => x.Id.ToString().Contains(value))
        },
        {
            nameof(TaskListDto.AssignedToId),
            (tasks, value) => tasks.Where(x => x.AssignedToId.ToString() == value)
        },
        {
            nameof(TaskListDto.CreatedById),
            (tasks, value) => tasks.Where(x => x.CreatedById.ToString() == value)
        },
        {
            nameof(TaskListDto.Status),
            (tasks, value) =>
            {
                if (value == "0") return tasks;
                return tasks.Where(x => ((int)x.Status & int.Parse(value)) != 0);
            }
        },
        {
            nameof(TaskListDto.Priority),
            (tasks, value) =>
            {
                if (value == "0") return tasks;
                return tasks.Where(x => ((int)x.Priority & int.Parse(value)) != 0);
            }
        },
        {
            nameof(TaskListDto.CreatedAt),
            (tasks, value) =>
            {
                var dates = value.Split('-');
                var hasDateFrom = DateTime.TryParse(dates[0], out DateTime dateFrom);
                var hasDateTo = DateTime.TryParse(dates[1], out DateTime dateTo);

                tasks = tasks.Where(x => !hasDateFrom || x.CreatedAt.Date >= dateFrom);
                tasks = tasks.Where(x => !hasDateTo || x.CreatedAt.Date <= dateTo);

                return tasks;
            }
        },
        {
            nameof(TaskListDto.Deadline),
            (tasks, value) =>
            {
                var dates = value.Split('-');
                var hasDateFrom = DateTime.TryParse(dates[0], out DateTime dateFrom);
                var hasDateTo = DateTime.TryParse(dates[1], out DateTime dateTo);

                tasks = tasks.Where(x => !hasDateFrom || x.Deadline >= dateFrom);
                tasks = tasks.Where(x => !hasDateTo || x.Deadline <= dateTo);

                return tasks;
            }
        }
    };
}
