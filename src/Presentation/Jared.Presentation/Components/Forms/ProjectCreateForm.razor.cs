using Jared.Presentation.Commons;
using Jared.Presentation.Requests.Projects.Create;
using Jared.Shared.Dtos.ProjectDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class ProjectCreateForm
{
    public ProjectDetailsDto Dto { get; set; } = new();

    private void cancel()
    {
        NotificationService.Information(NotificationHelper.PROJECT_CREATION_CANCELED);
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectCreateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.PROJECT_CREATION_FAILED);
        }

        NotificationService.Success(NotificationHelper.PROJECT_CREATION_SUCCESS);
        Close();
    }
}
