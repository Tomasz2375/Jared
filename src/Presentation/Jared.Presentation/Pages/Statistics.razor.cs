using Jared.Presentation.Requests.User.List;
using Jared.Presentation.Requests.WorkLogs.Statistics;
using Jared.Shared.Dtos.UserDtos;
using Jared.Shared.Dtos.WorkLogDtos;
using Jared.Shared.Enums;

namespace Jared.Presentation.Pages;

public partial class Statistics
{
    private List<WorkLogStatisticsDto> workLogsStatistics = new();
    private List<ProjectWorkLog> statistics = new();
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
        statistics.Clear();
        var daysInMonth = DateTime.DaysInMonth(year, (int)month);

        foreach (var wl in workLogsStatistics.GroupBy(x => x.ProjectId))
        {
            List<WorkPerDay> workPerDays = new();
            for (int i = 1; i <= daysInMonth; i++)
            {
                var ticks = wl.Where(x => x.WorkDate.Day == i).Sum(x => x.Time.Ticks);

                workPerDays.Add(new()
                {
                    Day = i.ToString(),
                    Time = new TimeSpan(ticks).TotalHours,
                });
            }

            ProjectWorkLog stat = new()
            {
                ProjectId = wl.First().ProjectId,
                Color = wl.First().Color,
                ProjectTitle = wl.First().ProjectTitle,
                WorkPerDay = workPerDays,
            };

            statistics.Add(stat);
        }

        statistics = statistics.OrderBy(x => x.ProjectId).ToList();
    }

    private sealed class ProjectWorkLog
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = default!;
        public string Color { get; set; } = default!;
        public List<WorkPerDay> WorkPerDay { get; set; } = new();
    }

    private sealed class WorkPerDay
    {
        public string Day { get; set; } = default!;
        public double Time { get; set; }
    }
}
