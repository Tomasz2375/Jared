using Jared.Client.Services;

namespace Jared.Client.Abstractions;

public interface IDialogService
{
    IReadOnlyList<DialogDefinition> Current { get; }

    event Action? OnDialogsUpdated;

    void Create<TDialog>(Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase;

    void Update<TDialog>(int id, Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase;

    public void Update(Type dialogType, int id, Dictionary<string, object>? parameters = null);

    Task<TResult?> Add<TDialog, TResult>(Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase<TResult>;
}
