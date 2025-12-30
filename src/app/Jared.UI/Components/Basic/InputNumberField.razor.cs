using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputNumberField
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    [Parameter]
    public bool Disabled { get; set; }

    protected override bool TryParseValueFromString(
        string? value,
        out int result,
        out string validationErrorMessage)
    {
        if (int.TryParse(value, out int number))
        {
            result = number;
            validationErrorMessage = null!;

            return true;
        }

        result = 0;
        validationErrorMessage = "Insert number";

        return false;
    }
}
