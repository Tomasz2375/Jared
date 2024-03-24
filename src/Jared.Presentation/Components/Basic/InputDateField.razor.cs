using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Jared.Presentation.Components.Basic;

public partial class InputDateField
{
    [Parameter]
    public Expression<Func<DateTime?>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }

    protected override bool TryParseValueFromString(string? value, out DateTime? result, out string validationErrorMessage)
    {
        result = DateTime.Parse(value!);
        validationErrorMessage = null!;
        return true;
    }
}
