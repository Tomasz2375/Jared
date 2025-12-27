using Jared.Client.Commons;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Jared.Dtos.WorkLogs;
using Jared.UI.Components.Dialog;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class TaskDetailsDetails
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;
    [Parameter]
    public List<ProjectListDto> Projects { get; set; } = new();
    [Parameter]
    public List<EpicListDto> Epics { get; set; } = new();
    [Parameter]
    public List<TaskListDto> Tasks { get; set; } = new();
    [Parameter]
    public List<UserListDto> Users { get; set; } = new();

    private async Task addWorkLog()
    {
        var dto = await DialogService.Add<WorkLogDialog, WorkLogAddDto>();
        if (dto is null)
        {
            return;
        }

        var user = await UserService.GetUserAsync();
        WorkLogListDto workLog = new()
        {
            Time = new(dto.Hours, dto.Minutes, 0),
            WorkDate = dto.WorkDate ?? DateTime.Now.Date,
            TaskId = Dto.Id,
            UserId = user.Id,
            UserFullName = user.FullName,
        };

        Dto.TotalWorkTime += workLog.Time;
        Dto.WorkLogs.Add(workLog);
    }
}
