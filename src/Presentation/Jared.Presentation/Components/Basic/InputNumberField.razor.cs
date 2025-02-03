using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Jared.Presentation.Components.Basic;

public partial class InputNumberField
{
    [Parameter]
    public Expression<Func<int>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }
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
        validationErrorMessage = "Error";
        return false;
    }
}
