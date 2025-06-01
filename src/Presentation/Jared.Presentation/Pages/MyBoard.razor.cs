using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.Requests.Tasks.Page;
using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Pages;

public partial class MyBoard
{
    [Parameter]
    public string Page { get; set; } = default!;

    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Query.Filter["Status"] = "7";
        switch (Page)
        {
            case nameof(Dashboard.Created):
                Query.Filter[nameof(TaskRootDto.CreatedById)] = UserService.GetUserId().ToString();
                break;
            case nameof(Dashboard.Assigned):
                Query.Filter[nameof(TaskRootDto.AssignedToId)] = UserService.GetUserId().ToString();
                break;
        }

        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new TaskPageQuery(query));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
