using Jared.Application.Commands.EpicCommands;
using Jared.Application.Dtos.EpicDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class EpicCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public EpicDetailsDto Dto { get; set; } = new();

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new EpicCreateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Create epic failed");
        }

        await CloseDialog.InvokeAsync();
    }
}
