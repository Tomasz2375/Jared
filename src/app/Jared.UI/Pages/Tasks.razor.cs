using Jared.Client.ColumnDefinitions;
using Jared.Client.Requests.Tasks.Page;
using Jared.Dtos.Tasks;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Pages;

public partial class Tasks
{
    [Parameter]
    public string ProjectId { get; set; } = default!;
    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Query.Filter["Status"] = "7";
        Query.Filter["ProjectId"] = ProjectId;

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
