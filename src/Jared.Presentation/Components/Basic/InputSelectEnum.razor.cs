using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Jared.Presentation.Components.Basic;

public partial class InputSelectEnum<TEnum> where TEnum : struct, Enum
{
    [Parameter]
    public Expression<Func<TEnum>> ValidationFor { get; set; } = default!;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public EventCallback<TEnum> EnumPropertyChanged { get; set; }

    protected override bool TryParseValueFromString(string? value, out TEnum result, out string validationErrorMessage)
    {
        result = (TEnum)Enum.Parse(typeof(TEnum).GetType(), value!);
        validationErrorMessage = null!;
        return true;
    }
}
