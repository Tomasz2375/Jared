using Jared.Application.Dtos.ProjectDtos;
using Jared.Presentation.Requests.Projects.Details;
using Jared.Presentation.Requests.Projects.Update;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class ProjectDetailsForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public int Id { get; set; }

    public ProjectDetailsDto Dto { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await getDetails(Id);
    }

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new ProjectUpdateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Save project failed");
        }

        await CloseDialog.InvokeAsync();
    }

    private async Task saveAndStay()
    {
        var result = await Mediator.Send(new ProjectUpdateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Save project failed");
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
