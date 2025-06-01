using Jared.Presentation.Commons;
using Jared.Presentation.Requests.Projects.Details;
using Jared.Presentation.Requests.Projects.Update;
using Jared.Shared.Dtos.ProjectDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

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
        NotificationService.Information(NotificationHelper.PROJECT_UPDATE_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectUpdateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.PROJECT_UPDATE_FAILED);
        }

        NotificationService.Success(NotificationHelper.PROJECT_UPDATE_SUCCESS);

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
            Console.WriteLine("Get project failed");
        }

        Dto = result.Data;
    }
}
