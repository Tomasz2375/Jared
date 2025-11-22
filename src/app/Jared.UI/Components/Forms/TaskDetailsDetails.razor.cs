using Jared.Client.Requests.Epics.List;
using Jared.Client.Requests.Projects.List;
using Jared.Client.Requests.Tasks.List;
using Jared.Client.Requests.User.List;
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

    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> tasks = new();
    private List<UserListDto> users = new();

    protected override async Task OnInitializedAsync()
    {
        await getTasksAsync();
        await getProjectsAsync();
        await getEpicsAsync();
        await getUsersAsync();
    }

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectListQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        projects = result.Data;
    }

    private async Task getEpicsAsync()
    {
        var result = await Mediator.Send(new EpicListQuery(Dto.ProjectId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get epic list");
            return;
        }

        epics = result.Data.ToList();
    }

    private async Task getTasksAsync()
    {
        var result = await Mediator.Send(new TaskListQuery(Dto.ProjectId, Dto.EpicId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get task list");
            return;
        }

        tasks = result.Data.ToList();
    }

    private async Task getUsersAsync()
    {
        var result = await Mediator.Send(new UserListQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get users list");
            return;
        }

        users = result.Data.ToList();
    }

    private async Task addWorkLog()
    {
        var dto = await DialogService.Add<WorkLogDialog, WorkLogAddDto>();
        if (dto is null)
        {
            return;
        }

        WorkLogListDto workLog = new()
        {
            Time = new(dto.Hours, dto.Minutes, 0),
            WorkDate = dto.WorkDate is null
                ? DateTime.Now.Date
                : (DateTime)dto.WorkDate,
            TaskId = Dto.Id,
            UserId = UserService.GetUserId(),
            UserFullName = UserService.GetUserName(),
        };

        Dto.TotalWorkTime += workLog.Time;
        Dto.WorkLogs.Add(workLog);
    }
}
