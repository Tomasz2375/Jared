using Jared.Client.Requests.Projects.List;
using Jared.UI.Components.Forms;

namespace Jared.UI.Components.Menu;

public partial class Tasks
{
    private Dictionary<string, string> projects = new();

    protected override async Task OnInitializedAsync()
    {
        await getProjectsAsync();
    }

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

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectListQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        projects = result.Data.ToDictionary(x => x.Id.ToString(), x => x.Title);
    }

    private void createTask()
    {
        DialogService.Create<TaskCreateForm>();
    }
}
