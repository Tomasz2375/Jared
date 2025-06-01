using Jared.Presentation.Components.Forms;
using Jared.Shared.Dtos.EpicDtos;

namespace Jared.Presentation.Components.Menu;

public partial class Epics
{
    private bool showUserMenu;

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

    private void createEpic()
    {
        DialogService.Create<EpicCreateForm>();
    }
}
