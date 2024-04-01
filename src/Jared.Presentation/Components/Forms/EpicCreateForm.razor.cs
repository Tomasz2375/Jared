using Jared.Application.Commands.EpicCommands;
using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Queries.ProjectQueries;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class EpicCreateForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public string? Title { get; set; }

    public EpicDetailsDto Dto { get; set; } = new();
    private List<ProjectListDto> projects = new();
    private Dictionary<int, string> projectsDictionary = new();

    protected override async Task OnInitializedAsync()
    {
        await getProjectsAsync();
    }

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
}
