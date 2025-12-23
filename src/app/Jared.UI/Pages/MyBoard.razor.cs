using Jared.Client.ColumnDefinitions;
using Jared.Contracts.Tasks;
using Jared.Core.Enums;
using Jared.Dtos.Tasks;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class MyBoard
{
    [Parameter]
    public string Page { get; set; } = default!;

    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var user = await UserService.GetUserAsync();
        Query.Filter["Status"] = "7";
        switch (Page)
        {
            case nameof(Dashboard.Created):
                Query.Filter[nameof(TaskRootDto.CreatedById)] = user.Id.ToString();
                break;
            case nameof(Dashboard.Assigned):
                Query.Filter[nameof(TaskRootDto.AssignedToId)] = user.Id.ToString();
                break;
        }

        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new TaskPageQuery(
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
