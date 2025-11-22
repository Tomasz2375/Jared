using Jared.Client.Commons;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;

namespace Jared.UI.Components.Forms;

public partial class TaskCreateForm
{
    public TaskDetailsDto Dto { get; set; } = new();
    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> tasks = new();
    private List<UserListDto> users = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await getProjectsAsync();
        await getEpicsAsync();
        await getTasksAsync();
        await getUsersAsync();

        Dto.Deadline = DateTime.Now.Date;
        Dto.Priority = Shared.Enums.Priority.Normal;
        Dto.Status = Shared.Enums.TaskStatus.Created;
        Dto.ParentId = null;
        Dto.CreatedById = UserService.GetUserId();
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
            Console.WriteLine("Error when get epics list");
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

    private void cancel()
    {
        NotificationService.Information(NotificationHelper.TASK_CREATION_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskCreateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.TASK_CREATION_FAILED);
        }

        NotificationService.Success(NotificationHelper.TASK_CREATION_SUCCESS);
        Close();
    }
}