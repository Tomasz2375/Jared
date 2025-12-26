using Jared.Client.Commons;
using Jared.Contracts.Projects;
using Jared.Dtos.Projects;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Forms;

public partial class ProjectDetailsForm
{
    [Parameter]
    public int Id { get; set; }

    public ProjectDetailsDto Dto { get; set; } = default!;
    private bool closeDialog;

    protected override async Task OnInitializedAsync()
    {
        await getDetails(Id);
    }

    private void cancel()
    {
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectUpdateCommand(Dto));
        if (result.Success)
        {
            NotificationService.Success(NotificationHelper.ProjectUpdated(Dto.Title));
        }
        else
        {
            NotificationService.Error(NotificationHelper.ProjectUpdateFailed(result.Error));
        }

        if (closeDialog)
        {
            Close();
        }
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new ProjectDetailsQuery(id));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.ProjectFetchFailed(result.Error));

            return;
        }

        Dto = result.Data;
    }
}
