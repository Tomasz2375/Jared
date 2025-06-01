using Jared.Presentation.Services;

namespace Jared.Presentation.Components.Abstractions;

public interface IDialogService
{
    IReadOnlyList<DialogDefinition> Current { get; }

    event Action? OnDialogsUpdated;

    Task<TResult?> Create<TDialog, TResult>(Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase<TResult>;

    void Update<TDialog>(int id, Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase;
}
