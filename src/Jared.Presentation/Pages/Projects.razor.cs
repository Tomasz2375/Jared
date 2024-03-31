using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Queries.ProjectQueries;
using Jared.Presentation.ColumnDefinitions;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

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
            query.Filter,
            query.SortingProperty,
            query.SortingDirection));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
