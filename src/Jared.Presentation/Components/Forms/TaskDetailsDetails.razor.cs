using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Application.Queries.ProjectQueries;
using Jared.Application.Queries.TaskQueries;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsDetails
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;

    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> parents = new();
    private Dictionary<int, string> projectsDictionary = new();
    private Dictionary<int, string> epicsDictionary = new();
    private Dictionary<int, string> parentDictionary = new();

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
            }
        }
    }
    public int? EpicId
    {
        get
        {
            return Dto.EpicId;
        }
        set
        {
            if (value != Dto.EpicId)
            {
                if (value == 0)
                {
                    value = null;
                }
                Dto.EpicId = value;
            }
        }
    }
    public int? ParentId
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
        await getTasksAsync();
        await getProjectsAsync();
        await getEpicsAsync();
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
            Console.WriteLine("Error when get epic list");
            return;
        }

        epics = result.Data.ToList();
        epicsDictionary = epics.ToDictionary(x => x.Id, x => x.Title);
    }

    private async Task getTasksAsync()
    {
        var result = await Mediator.Send(new TaskListQuery(Dto.ProjectId, Dto.EpicId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get task list");
            return;
        }

        parents = result.Data.ToList();
        parentDictionary = parents.ToDictionary(x => x.Id, x => x.Title);
    }
}
