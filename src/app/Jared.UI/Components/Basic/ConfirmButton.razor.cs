using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class ConfirmButton
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = default!;

    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter]
    public string? Class { get; set; }

    private string CssClasses => $"btn btn-primary {Class ?? string.Empty}";

    private async Task handleClick()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}
