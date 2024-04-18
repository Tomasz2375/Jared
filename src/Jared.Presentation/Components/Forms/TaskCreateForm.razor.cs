using Jared.Application.Commands.TaskCommand;
using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Application.Queries.ProjectQueries;
using Jared.Application.Queries.TaskQueries;
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
    private Dictionary<int, string> projectsDictionary = new();
    private Dictionary<int, string> epicsDictionary = new();
    private Dictionary<int, string> tasksDictionary = new();

    public int ProjectId
    {
        get
        {
            return Dto.ProjectId;
        }
        set
        {
            if (value != Dto.ProjectId)
            {
                Dto.ProjectId = value;
                epicsDictionary = epics
                    .Where(x => value == 0 || x.ProjectId == value)
                    .ToDictionary(x => x.Id, x => x.Title);
                tasksDictionary = tasks
                    .Where(x => value == 0 || x.ProjectId == value)
                    .ToDictionary(x => x.Id, x => x.Title);
                EpicId = 0;
                ParentTaskId = 0;
            }
        }
    }

    public int EpicId
    {
        get
        {
            return Dto.EpicId;
        }
        set
        {
            if (value != Dto.EpicId)
            {
                Dto.EpicId = value;

                tasksDictionary = tasks
                    .Where(x => value == 0 || x.EpicId == value)
                    .ToDictionary(x => x.Id, x => x.Title);
                ParentTaskId = 0;
            }
        }
    }

    public int? ParentTaskId
    {
        get
        {
            return Dto.ParentId;
        }
        set
        {
            if (value != Dto.ParentId)
            {
                if (value == 0)
                {
                    value = null;
                }
                Dto.ParentId = value;
            }
        }
    }

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
        projectsDictionary = projects.ToDictionary(x => x.Id, x => x.Title);
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
        epicsDictionary = epics.ToDictionary(x => x.Id, x => x.Title);
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
        tasksDictionary = tasks.ToDictionary(x => x.Id, x => x.Title);
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