using Jared.Presentation.Services;

namespace Jared.Presentation.Components.Abstractions;

public interface IDialogService
{
    IReadOnlyList<DialogDefinition> Current { get; }

    event Action? OnDialogsUpdated;

    void Create<TDialog>(Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase;

    void Update<TDialog>(int id, Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase;

    Task<TResult?> Add<TDialog, TResult>(Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase<TResult>;
}
