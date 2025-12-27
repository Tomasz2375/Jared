using Jared.Client.Commons;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class TaskDetailsForm
{
    [Parameter]
    public int Id { get; set; }

    public TaskDetailsDto Dto { get; set; } = new();
    private int tabNumber;
    private bool closeDialog;

    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> tasks = new();
    private List<UserListDto> users = new();

    protected override async Task OnInitializedAsync()
    {
        await loadAllData();
    }

    private async Task loadAllData()
    {
        await getDetails(Id);
        await getProjects();
        await getEpics(Dto.ProjectId);
        await getTasks(Dto.ProjectId, Dto.EpicId);
        await getUsers();
    }

    private void cancel()
    {
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskUpdateCommand(Dto));

        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.TaskUpdateFailed(result.Error));

            return;
        }

        NotificationService.Success(NotificationHelper.TaskUpdated(Dto.Title));

        if (closeDialog)
        {
            Close();

            return;
        }

        await loadAllData();
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new TaskDetailsQuery(id));
        if (!result.Success)
        {
            NotificationService.Warning(NotificationHelper.TaskFetchFailed(result.Error));

            return;
        }

        Dto = result.Data;
    }

    private async Task getProjects()
    {
        var result = await Mediator.Send(new ProjectPageQuery());
        if (!result.Success)
        {
            NotificationService.Warning(NotificationHelper.ProjectsFetchFailed(result.Error));

            return;
        }

        projects = result.Data.Items;
    }

    private async Task getEpics(int projectId)
    {
        var result = await Mediator.Send(new EpicPageQuery(projectId));
        if (!result.Success)
        {
            NotificationService.Warning(NotificationHelper.EpicsFetchFailed(result.Error));

            return;
        }

        epics = result.Data.Items;
    }

    private async Task getTasks(int projectId, int? epicId)
    {
        var filters = new Dictionary<string, string?>
        {
            { nameof(TaskListDto.ProjectId), projectId.ToString() },
            { nameof(TaskListDto.EpicId), epicId?.ToString() },
        };
        var result = await Mediator.Send(new TaskPageQuery(filters: filters));
        if (!result.Success)
        {
            NotificationService.Warning(NotificationHelper.TasksFetchFailed(result.Error));

            return;
        }

        tasks = result.Data.Items;
    }

    private async Task getUsers()
    {
        var result = await Mediator.Send(new UserListQuery());
        if (!result.Success)
        {
            NotificationService.Warning($"Failed to load users. '{result.Error}'");

            return;
        }

        users = result.Data.ToList();
    }
}
