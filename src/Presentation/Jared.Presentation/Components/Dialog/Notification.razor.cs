using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Dialog;

public partial class Notification
{
    protected override void OnInitialized()
    {
        NotificationService.OnChange += handleChange;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            NotificationService.OnChange -= handleChange;
        }
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

    ~Notification()
    {
        Dispose(false);
    }
}
