using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Jared.Presentation.Components.Basic;

public partial class InputTextAreaField
{
    [Parameter]
    public Expression<Func<int>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public int Row { get; set; } = 5;

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
        result = value!;
        validationErrorMessage = null!;
        return true;
    }
}
