using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.Requests.Projects.Page;
using Jared.Shared.Dtos.ProjectDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

public partial class Projects : ComponentBase
{
    public ProjectPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();
    private int showDialogWithId;

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new ProjectPageQuery(query));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
