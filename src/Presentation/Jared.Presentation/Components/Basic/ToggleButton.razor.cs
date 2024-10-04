using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Basic;

public partial class ToggleButton
{
    [Parameter]
    public bool IsToggled { get; set; }

    [Parameter]
    public EventCallback<bool> IsToggledChanged { get; set; }

    private void Toggle()
    {
        IsToggled = !IsToggled;
        IsToggledChanged.InvokeAsync(IsToggled);
    }

    private string buttonClass => IsToggled ? "toggle-on" : "toggle-off";
    private string buttonText => IsToggled ? "Dark" : "Light";
}
