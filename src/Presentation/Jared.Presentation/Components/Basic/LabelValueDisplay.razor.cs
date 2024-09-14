using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Basic;

public partial class LabelValueDisplay
{
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    [EditorRequired]
    public string? Value { get; set; }
}
