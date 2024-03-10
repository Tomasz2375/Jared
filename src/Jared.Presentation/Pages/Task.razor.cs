using Jared.Application.Dtos.TaskDto;
using Jared.Application.Queries.TaskQueries;
using Jared.Presentation.ColumnDefinitions;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

public partial class Task : ComponentBase
{
    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async System.Threading.Tasks.Task OnInitializedAsync()
    {
        await SendPageQuery(Query);
    }

    private async System.Threading.Tasks.Task SendPageQuery(Query query)
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

        StateHasChanged();
    }
}
