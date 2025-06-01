namespace Jared.Presentation.Components.Dialog;

public partial class DialogContainer
{
    protected override void OnInitialized()
    {
        DialogService.OnDialogsUpdated += StateHasChanged;
    }

    public void Dispose()
    {
        DialogService.OnDialogsUpdated -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
