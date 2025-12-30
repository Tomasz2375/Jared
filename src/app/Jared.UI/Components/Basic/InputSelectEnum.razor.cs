using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputSelectEnum<TEnum>
    where TEnum : Enum
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    [Parameter]
    public EventCallback<TEnum> EnumPropertyChanged { get; set; }

    protected override bool TryParseValueFromString(string? value, out TEnum result, out string validationErrorMessage)
    {
        result = (TEnum)Enum.Parse(typeof(TEnum), value!);
        validationErrorMessage = null!;
        return true;
    }
}
