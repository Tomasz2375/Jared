using Jared.Client.Commons;
using Jared.Contracts.Epics;
using Jared.Contracts.Projects;
using Jared.Dtos.Epics;
using Jared.Dtos.Projects;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class EpicDetailsForm
{
    private List<ProjectListDto> projects = default!;
    private List<EpicListDto> epics = default!;
    private Dictionary<int, string> projectsDictionary = default!;
    private Dictionary<int, string> epicsDictionary = default!;
    private bool closeDialog;

    [Parameter]
    public int Id { get; set; }

    public EpicDetailsDto Dto { get; set; } = default!;

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
        await getDetails(Id);
        await getProjectsAsync();
        await getEpicsAsync();
    }

    private void cancel()
    {
        NotificationService.Information(NotificationHelper.EPIC_UPDATE_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new EpicUpdateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.EPIC_UPDATE_FAILED);
        }

        NotificationService.Success(NotificationHelper.EPIC_UPDATE_SUCCESS);
        if (closeDialog)
        {
            Close();
        }
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new EpicDetailsQuery(id));

        if (!result.Success)
        {
            Console.WriteLine("Get epic failed");
        }

        Dto = result.Data;
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

        epics = result.Data;
        epicsDictionary = epics.ToDictionary(x => x.Id, x => x.Title);
    }
}
