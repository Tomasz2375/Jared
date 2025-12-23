using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class TaskDetailsWorkLog
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;
    private UserDto user = default!;

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
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
