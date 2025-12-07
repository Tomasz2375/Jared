using Microsoft.AspNetCore.Components;

namespace Jared.Client.Abstractions;

public class DialogBase : ComponentBase
{
    [Parameter]
    public EventCallback OnClose { get; set; }

    protected void Close() => OnClose.InvokeAsync();
}

public abstract class DialogBase<TResult> : ComponentBase
{
    [Parameter]
    public EventCallback<TResult?> OnClose { get; set; }

    protected void Close(TResult? result) => OnClose.InvokeAsync(result);
    protected void Cancel() => OnClose.InvokeAsync(default);
}
