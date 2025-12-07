using Jared.Dtos.WorkLogs;

namespace Jared.UI.Components.Dialog;

public partial class WorkLogDialog
{
    public WorkLogAddDto WorkLog { get; set; } = new();

    private void cancel()
    {
        Close(default);
    }

    private void save()
    {
        Close(WorkLog);
    }
}
