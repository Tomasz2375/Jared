using Jared.Client.Commons;
using Jared.Client.Requests.Epics.Create;
using Jared.Client.Requests.Epics.List;
using Jared.Client.Requests.Projects.List;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;

namespace Jared.UI.Components.Forms;

public partial class EpicCreateForm
{
    public EpicDetailsDto Dto { get; set; } = new();
    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private Dictionary<int, string> projectsDictionary = new();
    private Dictionary<int, string> epicsDictionary = new();

    public int? EpicId
    {
        get => Dto.ParentId;
        set
        {
            if (Dto.ParentId != value)
            {
                Dto.ParentId = value;
                var epic = epics.Find(x => x.Id == value);
                if (epic != null)
                {
                    Dto.ProjectId = epic.ProjectId;
                    projectsDictionary = projects
                        .Where(x => x.Id == epic.ProjectId)
                        .ToDictionary(x => x.Id, x => x.Title);
                }
                else
                {
                    projectsDictionary = projects
                        .ToDictionary(x => x.Id, x => x.Title);
                }
            }
        }
    }

    public int ProjectId
    {
        get => Dto.ProjectId;
        set
        {
            if (Dto.ProjectId != value)
            {
                Dto.ProjectId = value;
                setEpics();
            }

            if (value == 0)
            {
                epicsDictionary = epics
                    .ToDictionary(x => x.Id, x => x.Title);
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await getProjectsAsync();
        await getEpicsAsync();
    }

    private void cancel()
    {
        NotificationService.Information(NotificationHelper.EPIC_CREATION_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new EpicCreateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.EPIC_CREATION_FAILED);
        }

        NotificationService.Success(NotificationHelper.EPIC_CREATION_SUCCESS);
        Close();
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

    private void setEpics()
    {
        epicsDictionary = epics
            .Where(x => x.ProjectId == Dto.ProjectId)
            .ToDictionary(x => x.Id, x => x.Title);
    }
}
