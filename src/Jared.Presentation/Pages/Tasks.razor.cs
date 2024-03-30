using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.TaskQueries;
using Jared.Presentation.ColumnDefinitions;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

public partial class Tasks : ComponentBase
{
    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new TaskListQuery(
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
