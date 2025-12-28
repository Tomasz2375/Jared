using Jared.Client.Commons;
using Jared.Contracts.Projects;
using Jared.Contracts.Tasks;
using Jared.Contracts.Users;
using Jared.Dtos.Projects;
using Jared.Dtos.Tasks;
using Jared.Dtos.Users;

namespace Jared.UI.Components.Forms;

public partial class TaskCreateForm
{
    public TaskDetailsDto Dto { get; set; } = new();
    private UserDto user = default!;
    private List<ProjectListDto> projects = new();
    private List<TaskListDto> tasks = new();
    private List<UserListDto> users = new();

    protected override async Task OnInitializedAsync()
    {
        user = await UserService.GetUserAsync();
        await getProjectsAsync();
        await getTasksAsync();
        await getUsersAsync();

        Dto.Deadline = DateTime.Now.Date;
        Dto.Priority = Core.Enums.Priority.Normal;
        Dto.Status = Core.Enums.TaskStatus.Created;
        Dto.ParentId = null;
        Dto.CreatedById = user.Id;
    }

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectPageQuery());
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.ProjectsFetchFailed(result.Error));

            return;
        }

        projects = result.Data.Items;
    }

    private async Task getTasksAsync()
    {
        Dictionary<string, string?> filters = new()
        {
            { nameof(TaskListDto.ProjectId), Dto.ProjectId.ToString() },
        };
        var result = await Mediator.Send(new TaskPageQuery(filters: filters));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.TasksFetchFailed(result.Error));

            return;
        }

        tasks = result.Data.Items;
    }

    private async Task getUsersAsync()
    {
        var result = await Mediator.Send(new UserListQuery());

        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.UsersFetchFailed(result.Error));

            return;
        }

        users = result.Data.ToList();
    }

    private void cancel()
    {
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskCreateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.TaskCreationFailed(result.Error));

            return;
        }

        NotificationService.Success(NotificationHelper.TaskCreated(Dto.Title));
        cancel();
    }

    private async Task onProjectChanged(int projectId)
    {
        Dto.ProjectId = projectId;
        Dto.ParentId = null;

        await getTasksAsync();
    }
}