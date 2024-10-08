﻿using Jared.Presentation.Requests.Tasks.Details;
using Jared.Presentation.Requests.Tasks.Update;
using Jared.Shared.Dtos.TaskDtos;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public int Id { get; set; }

    public TaskDetailsDto Dto { get; set; } = new();
    private int tabNumber;
    private bool closeDialog;

    protected override async Task OnInitializedAsync()
    {
        await getDetails(Id);
    }

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new TaskUpdateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Save task failed");
        }

        if (closeDialog)
        {
            await CloseDialog.InvokeAsync();
        }
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new TaskDetailsQuery(id));

        if (!result.Success)
        {
            Console.WriteLine("Get task details failed");
        }

        Dto = result.Data;
    }
}
