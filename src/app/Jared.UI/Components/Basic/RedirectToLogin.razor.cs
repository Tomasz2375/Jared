namespace Jared.UI.Components.Basic;

public partial class RedirectToLogin
{
    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo("login");
    }
}
