using Microsoft.AspNetCore.Components;

namespace Jared.UI.Components.Basic;

public partial class InputSelectList<TKey>
{
    [Parameter]
    public Dictionary<int, string> Items { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string? Id { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string? Placeholder { get; set; } = default!;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool ShowDefault { get; set; } = true;

    [Parameter]
    public EventCallback<TKey> ValuePropertyChanged { get; set; }

    protected override bool TryParseValueFromString(string? value, out TKey result, out string validationErrorMessage)
    {
        result = default!;
        validationErrorMessage = null!;

        return true;
    }
}
