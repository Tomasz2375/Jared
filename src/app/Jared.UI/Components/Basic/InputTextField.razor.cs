using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputTextField
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
    public bool IsPassword { get; set; }

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value!;
        validationErrorMessage = null!;

        return true;
    }
}
