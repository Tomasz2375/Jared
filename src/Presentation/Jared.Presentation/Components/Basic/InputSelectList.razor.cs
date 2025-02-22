using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Basic;

public partial class InputSelectList<TKey>
{
    [Parameter]
    public Dictionary<int, string> Items { get; set; } = default!;
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public bool Disabled { get; set; }
    [Parameter]
    public bool ShowDefault { get; set; }
    [Parameter]
    public Expression<Func<TKey>> ValidationFor { get; set; } = default!;
    [Parameter]
    public EventCallback<TKey> ValuePropertyChanged { get; set; }

    protected override bool TryParseValueFromString(string? value, out TKey result, out string validationErrorMessage)
    {
        result = default!;
        validationErrorMessage = null!;
        return true;
    }
}
