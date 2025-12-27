using Jared.Client.Commons;
using Jared.Contracts.Projects;
using Jared.Dtos.Projects;

namespace Jared.UI.Components.Forms;

public partial class ProjectCreateForm
{
    public ProjectDetailsDto Dto { get; set; } = new();

    private void cancel()
    {
        Close();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectCreateCommand(Dto));
        if (!result.Success)
        {
            NotificationService.Error(NotificationHelper.ProjectCreationFailed(result.Error));

            return;
        }

        NotificationService.Success(NotificationHelper.ProjectCreated(Dto.Title));
        Close();
    }
}
