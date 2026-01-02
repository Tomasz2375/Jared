using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputTextAreaField
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    [Parameter]
    public int Rows { get; set; } = 5;

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value!;
        validationErrorMessage = null!;
        return true;
    }
}
