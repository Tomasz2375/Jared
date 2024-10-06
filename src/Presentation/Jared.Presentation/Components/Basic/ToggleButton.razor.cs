using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Basic;

public partial class ToggleButton
{
    [Parameter]
    public bool IsToggled { get; set; }

    [Parameter]
    public string Color { get; set; } = default!;

    [Parameter]
    public EventCallback<bool> IsToggledChanged { get; set; }
    [Parameter]
    public EventCallback<string> OnColorChange { get; set; }

    private void Toggle()
    {
        IsToggled = !IsToggled;
        IsToggledChanged.InvokeAsync(IsToggled);
    }

    private async Task changeColor(ChangeEventArgs e)
    {
        var color = e.Value!.ToString();
        await OnColorChange.InvokeAsync(color);
    }

    private string buttonClass => IsToggled ? "toggle-on" : "toggle-off";
    private string buttonText => IsToggled ? "Dark" : "Light";
}
