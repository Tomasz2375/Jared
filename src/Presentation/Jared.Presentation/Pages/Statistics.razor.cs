using Jared.Presentation.Requests.User.List;
using Jared.Presentation.Requests.WorkLogs.Statistics;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Dtos.WorkLogDtos;
using Jared.Shared.Enums;

namespace Jared.Presentation.Pages;

public partial class Statistics
{
    private List<WorkLogStatisticsDto> workLogsStatistics = new();
    private List<MonthWork> monthWorks = new();
    private List<ProjectWork> projectWorks = new();
    private List<TaskWork> taskWorks = new();
    private List<UserListDto> users = new();

    private Month month { get; set; } = (Month)DateTime.Now.Month;

    private int year { get; set; } = DateTime.Now.Year;
    private int user { get; set; }
    private string userRole { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        user = UserService.GetUserId();
        userRole = UserService.GetUserRole();
        users = await getUsersAsync();
        await getWorkLogs(user, (int)month, year);
    }

    private static Dictionary<int, string> getYearDictionary()
    {
        var currentYear = DateTime.Now.Year;

        return new()
        {
            { currentYear, currentYear.ToString() },
            { currentYear - 1, (currentYear - 1).ToString() },
            { currentYear - 2, (currentYear - 2).ToString() },
            { currentYear - 3, (currentYear - 3).ToString() },
            { currentYear - 4, (currentYear - 4).ToString() },
        };
    }

    private Dictionary<int, string> getUsersDictionary()
    {
        if (userRole == "Admin" || userRole == "Manager")
        {
            return users.ToDictionary(x => x.Id, x => $"{x.FirstName} {x.LastName}");
        }

        return new()
        {
            { UserService.GetUserId(), UserService.GetUserName() },
        };
    }

    private async Task<List<UserListDto>> getUsersAsync()
    {
        if (userRole == "User")
        {
            return new();
        }

        var result = await Mediator.Send(new UserListQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get users list");
            return new();
        }

        return result.Data;
    }

    private async Task getWorkLogs(int user, int month, int year)
    {
        var result = await Mediator
            .Send(new WorkLogStatisticsQuery(user, month, year));

        if (!result.Success)
        {
            return;
        }

        workLogsStatistics = result.Data;

        prepareWorkTimeStatistics();
    }

    private void prepareWorkTimeStatistics()
    {
        monthWorks.Clear();
        projectWorks.Clear();
        taskWorks.Clear();
        var daysInMonth = DateTime.DaysInMonth(year, (int)month);

        foreach (var workLog in workLogsStatistics.GroupBy(x => new { x.ProjectId, x.ProjectTitle, x.Color }))
        {
            List<DayWork> workPerDays = new();
            for (int i = 1; i <= daysInMonth; i++)
            {
                var ticks = workLog.Where(x => x.WorkDate.Day == i).Sum(x => x.Time.Ticks);

                workPerDays.Add(new()
                {
                    Day = i.ToString(),
                    Time = new TimeSpan(ticks).TotalHours,
                });
            }

            MonthWork monthWork = new()
            {
                ProjectId = workLog.First().ProjectId,
                Color = workLog.First().Color,
                ProjectTitle = workLog.First().ProjectTitle,
                WorkPerDay = workPerDays,
            };

            monthWorks.Add(monthWork);

            TimeSpan totalProjectTime = new TimeSpan(workLog.Sum(x => x.Time.Ticks));
            ProjectWork workPerProject = new()
            {
                ProjectTitle = $"{workLog.Key.ProjectTitle} [{totalProjectTime.Hours}h {totalProjectTime.Minutes}min]",
                Color = workLog.Key.Color,
                Time = totalProjectTime.TotalHours,
            };

            projectWorks.Add(workPerProject);
        }

        foreach (var workLog in workLogsStatistics.GroupBy(x => new { x.TaskId, x.TaskCode, x.TaskTitle, x.ProjectTitle }))
        {
            taskWorks.Add(new()
            {
                TaskId = workLog.Key.TaskId,
                TaskCode = workLog.Key.TaskCode,
                TaskTitle = workLog.Key.TaskTitle,
                ProjectTitle = workLog.Key.ProjectTitle,
                Time = new TimeSpan(workLog.Sum(x => x.Time.Ticks)),
            });
        }

        projectWorks = projectWorks.OrderBy(x => x.ProjectTitle).ToList();
        monthWorks = monthWorks.OrderBy(x => x.ProjectId).ToList();
        taskWorks = taskWorks.OrderByDescending(x => x.Time).ToList();
    }

    private sealed class MonthWork
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = default!;
        public string Color { get; set; } = default!;
        public List<DayWork> WorkPerDay { get; set; } = new();
    }

    private sealed class DayWork
    {
        public string Day { get; set; } = default!;
        public double Time { get; set; }
    }

    private sealed class ProjectWork
    {
        public string ProjectTitle { get; set; } = default!;
        public double Time { get; set; }
        public string Color { get; set; } = default!;
    }

    private sealed class TaskWork
    {
        public int TaskId { get; set; }
        public string TaskCode { get; set; } = default!;
        public string TaskTitle { get; set; } = default!;
        public string ProjectTitle { get; set; } = default!;
        public TimeSpan Time { get; set; } = default!;
    }
}
