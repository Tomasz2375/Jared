using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Presentation.Requests.Epics.List;
using Jared.Presentation.Requests.Projects.List;
using Jared.Presentation.Requests.Tasks.Create;
using Jared.Presentation.Requests.Tasks.List;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public Application.Dtos.TaskDtos.TaskDetailsDto Dto { get; set; } = new();
    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> tasks = new();

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();
        await getProjectsAsync();
        await getEpicsAsync();
        await getTaskAsync();

        Dto.Deadline = DateTime.Now.Date;
        Dto.Priority = Domain.Enums.Priority.Normal;
        Dto.Status = Domain.Enums.TaskStatus.Created;
        Dto.ParentId = null;
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
            Console.WriteLine("Error when get project list");
            return;
        }

        epics = result.Data.ToList();
    }

    private async Task getTaskAsync()
    {
        var result = await Mediator.Send(new TaskListQuery(Dto.ProjectId, Dto.EpicId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        tasks = result.Data.ToList();
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