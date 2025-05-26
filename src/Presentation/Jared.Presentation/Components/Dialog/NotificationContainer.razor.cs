namespace Jared.Presentation.Components.Dialog;

public partial class NotificationContainer
{
    protected override void OnInitialized()
    {
        NotificationService.OnChange += handleChange;
    }

    public void Dispose()
    {
        NotificationService.OnChange -= handleChange;
        GC.SuppressFinalize(this);
    }

    private static string getIconClass(string type)
    {
        return type switch
        {
            "success" => "bi bi-check-circle",
            "information" => "bi bi-info-circle",
            "warning" => "bi bi-exclamation-triangle",
            "error" => "bi bi-exclamation-octagon",
            _ => "bi bi-question-circle"
        };
    }

    private async void handleChange()
    {
        await InvokeAsync(StateHasChanged);
    }
}
