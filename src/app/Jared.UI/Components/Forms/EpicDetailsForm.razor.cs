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
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new EpicUpdateCommand(Dto));
        if (result.Success)
        {
            NotificationService.Success(NotificationHelper.EpicUpdated(Dto.Title));
        }
        else
        {
            NotificationService.Error(NotificationHelper.EpicCreationFailed(result.Error));
        }

        if (closeDialog)
        {
            cancel();
        }
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new EpicDetailsQuery(id));

        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.EpicFetchFailed(result.Error));

            return;
        }

        Dto = result.Data;
    }

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectPageQuery());
        if (!result.Success)
        {
            NotificationService.Warning(NotificationHelper.ProjectsFetchFailed(result.Error));

            return;
        }

        projects = result.Data.Items;
        projectsDictionary = projects.ToDictionary(x => x.Id, x => x.Title);
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
            NotificationService.Warning(NotificationHelper.EpicsFetchFailed(result.Error));

            return;
        }

        epics = result.Data.Items;
        epicsDictionary = epics.ToDictionary(x => x.Id, x => x.Title);
    }
}
