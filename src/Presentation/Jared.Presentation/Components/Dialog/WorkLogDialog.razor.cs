using Jared.Shared.Dtos.WorkLogDtos;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Dialog;

public partial class WorkLogDialog
{
    [Parameter]
    public EventCallback<WorkLogAddDto?> CloseDialog { get; set; }
    public WorkLogAddDto WorkLog { get; set; } = new();

    private void cancel()
    {
        CloseDialog.InvokeAsync(null);
    }

    private void save()
    {
        CloseDialog.InvokeAsync(WorkLog);
    }
}
