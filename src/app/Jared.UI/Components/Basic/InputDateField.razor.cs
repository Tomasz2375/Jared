using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputDateField<TValue>
{
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Placeholder { get; set; } = default!;

    [Parameter]
    public bool Disabled { get; set; }

    protected override string FormatValueAsString(TValue? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        if (value is DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        throw new InvalidOperationException(
            $"{GetType()} obsługuje tylko DateTime i DateTime?");
    }

    protected override bool TryParseValueFromString(
        string? value,
        out TValue result,
        out string validationErrorMessage)
    {
        validationErrorMessage = null!;

        if (string.IsNullOrWhiteSpace(value))
        {
            result = default!;
            return true; // dla DateTime? => null
        }

        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            result = (TValue)(object)date;
            return true;
        }

        result = default!;
        validationErrorMessage = "Nieprawidłowa data";
        return false;
    }

    protected override void OnInitialized()
    {
        var type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (type != typeof(DateTime))
        {
            throw new InvalidOperationException(
                "InputDateField obsługuje tylko DateTime lub DateTime?");
        }
    }
}
