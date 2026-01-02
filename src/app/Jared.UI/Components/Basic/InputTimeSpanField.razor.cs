using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputTimeSpanField
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public EventCallback OnEdit { get; set; }

    protected override bool TryParseValueFromString(string? value, out TimeSpan result, out string validationErrorMessage)
    {
        result = TimeSpan.Parse(value!);
        validationErrorMessage = null!;
        return true;
    }

    private async Task handleOnInput()
    {
        await OnEdit.InvokeAsync(null);
    }
}
