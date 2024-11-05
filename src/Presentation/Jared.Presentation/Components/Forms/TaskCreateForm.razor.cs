using Jared.Presentation.Requests.Epics.List;
using Jared.Presentation.Requests.Projects.List;
using Jared.Presentation.Requests.Tasks.Create;
using Jared.Presentation.Requests.Tasks.List;
using Jared.Presentation.Requests.User.List;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.UserDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

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
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskCreateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Create task failed");
        }

        await CloseDialog.InvokeAsync();
    }
}