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
        var result = await Mediator.Send(new ProjectPageQuery());
        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        projects = result.Data.Items;
    }

    private async Task getEpicsAsync()
    {
        Dictionary<string, string?> filters = new()
        {
            { nameof(EpicListDto.ProjectId), Dto.ProjectId.ToString() },
        };
        var result = await Mediator.Send(new EpicPageQuery(filters: filters));
        if (!result.Success)
        {
            Console.WriteLine("Error when get epics list");
            return;
        }

        epics = result.Data.Items;
    }

    private async Task getTasksAsync()
    {
        Dictionary<string, string?> filters = new()
        {
            { nameof(TaskListDto.ProjectId), Dto.ProjectId.ToString() },
            { nameof(TaskListDto.EpicId), Dto.EpicId.ToString() },
        };
        var result = await Mediator.Send(new TaskPageQuery(filters: filters));
        if (!result.Success)
        {
            Console.WriteLine("Error when get task list");
            return;
        }

        tasks = result.Data.Items;
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

        var user = await UserService.GetUserAsync();
        WorkLogListDto workLog = new()
        {
            Time = new(dto.Hours, dto.Minutes, 0),
            WorkDate = dto.WorkDate is null
                ? DateTime.Now.Date
                : (DateTime)dto.WorkDate,
            TaskId = Dto.Id,
            UserId = user.Id,
            UserFullName = user.FullName,
        };

        Dto.TotalWorkTime += workLog.Time;
        Dto.WorkLogs.Add(workLog);
    }
}
