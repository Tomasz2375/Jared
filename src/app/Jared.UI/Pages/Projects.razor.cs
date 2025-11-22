using Jared.Client.ColumnDefinitions;
using Jared.Contracts.Projects;
using Jared.Dtos.Projects;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class Projects : ComponentBase
{
    public ProjectPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new ProjectPageQuery(
            query.Page,
            query.PageSize,
            query.SortingProperty,
            query.SortingDirection,
            query.Filter));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
