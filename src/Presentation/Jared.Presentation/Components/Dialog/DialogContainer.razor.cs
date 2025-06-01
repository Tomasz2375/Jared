namespace Jared.Presentation.Components.Dialog;

public partial class DialogContainer
{
    protected override void OnInitialized()
    {
        DialogService.OnDialogsUpdated += handleChange;
    }

    public void Dispose()
    {
        DialogService.OnDialogsUpdated -= handleChange;
        GC.SuppressFinalize(this);
    }

    private async void handleChange()
    {
        await InvokeAsync(StateHasChanged);
    }
}
