using Jared.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

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
    public Expression<Func<TKey>> ValidationFor { get; set; } = default!;
    [Parameter]
    public EventCallback<TKey> ValuePropertyChanged { get; set; }

    protected override bool TryParseValueFromString(string? value, out TKey result, out string validationErrorMessage)
    {
        result = default(TKey)!;
        validationErrorMessage = null!;
        return true;
    }
}
