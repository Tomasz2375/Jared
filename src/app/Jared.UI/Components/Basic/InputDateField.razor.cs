using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputDateField
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    protected override bool TryParseValueFromString(string? value, out DateTime? result, out string validationErrorMessage)
    {
        result = DateTime.Parse(value!);
        validationErrorMessage = null!;
        return true;
    }
}
