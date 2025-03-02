using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.WorkLogDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsWorkLog
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;
    private int userId;

    protected override void OnInitialized()
    {
        userId = UserService.GetUserId();
    }

    private void deleteWorkLog(WorkLogListDto worklog)
    {
        worklog.Delete = true;

        calculeteWorkTime();
    }

    private void restoreWorkLog(WorkLogListDto worklog)
    {
        worklog.Delete = false;

        calculeteWorkTime();
    }

    private void calculeteWorkTime()
    {
        Dto.TotalWorkTime = new TimeSpan(
            Dto.WorkLogs.Where(x => !x.Delete).Sum(x => x.Time.Ticks));
    }
}
