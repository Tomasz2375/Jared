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

    private async void handleChange()
    {
        await InvokeAsync(StateHasChanged);
    }

    ~Notification()
    {
        Dispose(false);
    }
}
