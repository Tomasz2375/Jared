using Jared.Application.Dtos.ProjectDtos;
using Jared.Presentation.CQRS.Projects.Create;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class ProjectCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public ProjectDetailsDto Dto { get; set; } = new();

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectCreateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Create task failed");
        }

        await CloseDialog.InvokeAsync();
    }
}
