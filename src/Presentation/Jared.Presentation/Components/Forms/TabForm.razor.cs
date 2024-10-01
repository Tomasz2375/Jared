using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TabForm
{
    [Parameter]
    public int Id { get; set; }
    [Parameter]
    public string Label { get; set; } = default!;
    [Parameter]
    public int ActiceTab { get; set; } = default!;
    [Parameter]
    public EventCallback<int> OnTabSelected { get; set; }

    private void selectTab()
    {
        OnTabSelected.InvokeAsync(Id);
    }
}
