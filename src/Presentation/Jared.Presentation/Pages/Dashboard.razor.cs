﻿using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.Requests.Tasks.Page;
using Jared.Shared.Dtos.TaskDtos;

namespace Jared.Presentation.Pages;

public partial class Dashboard
{
    public TaskPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();
    private int showDialogWithId;

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        if (!query.Filter!.ContainsKey("Status"))
        {
            query.Filter!.Add("Status", "6");
        }

        query.Filter.Add(nameof(TaskRootDto.AssignedToId), UserService.GetUserId().ToString());

        var result = await Mediator.Send(new TaskPageQuery(query));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
