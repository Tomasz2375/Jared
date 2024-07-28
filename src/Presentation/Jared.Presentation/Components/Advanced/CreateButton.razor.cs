using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Advanced;

public partial class CreateButton
{
    [Parameter]
    public EventCallback OpenTaskDialog { get; set; }
    [Parameter]
    public EventCallback OpenEpicDialog { get; set; }
    [Parameter]
    public EventCallback OpenProjectDialog { get; set; }

    private bool showUserMenu = false;

    private string userMenuCssClass => showUserMenu ? "show-menu" : string.Empty;

    private void toggleUserMenu()
    {
        showUserMenu = !showUserMenu;
    }

    private async Task hideUserMemu()
    {
        await Task.Delay(100);
        showUserMenu = false;
    }

    private async Task openTaskDialog()
    {
        await OpenTaskDialog.InvokeAsync();
    }

    private async Task openEpicDialog()
    {
        await OpenEpicDialog.InvokeAsync();
    }

    private async Task openProjectDialog()
    {
        await OpenProjectDialog.InvokeAsync();
    }
}
