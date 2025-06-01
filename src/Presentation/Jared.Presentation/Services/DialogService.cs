using Jared.Presentation.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Services;

public class DialogService : IDialogService
{
    public event Action? OnDialogsUpdated;
    private readonly List<DialogDefinition> dialogs = new();
    public IReadOnlyList<DialogDefinition> Current => dialogs;

    public void Create<TDialog>(
    Dictionary<string, object>? parameters = null)
    where TDialog : DialogBase
    {
        parameters ??= new();
        parameters[nameof(DialogBase.OnClose)] = EventCallback.Factory.Create(this, () =>
        {
            removeDialog(typeof(TDialog));
        });

        addDialog(typeof(TDialog), parameters);
    }

    public void Update<TDialog>(int id, Dictionary<string, object>? parameters = null)
        where TDialog : DialogBase
    {
        parameters ??= new();
        parameters["Id"] = id;
        parameters[nameof(DialogBase.OnClose)] = EventCallback.Factory.Create(this, () =>
        {
            removeDialog(typeof(TDialog));
        });

        addDialog(typeof(TDialog), parameters);
    }

    public Task<TResult?> Add<TDialog, TResult>(
            Dictionary<string, object>? parameters = null)
            where TDialog : DialogBase<TResult>
    {
        TaskCompletionSource<TResult?> taskCompletionSource = new();

        parameters ??= new();
        parameters[nameof(DialogBase<TResult>.OnClose)] = EventCallback.Factory.Create<TResult?>(this, (result) =>
        {
            removeDialog(typeof(TDialog));
            taskCompletionSource.SetResult(result);
        });

        addDialog(typeof(TDialog), parameters);

        return taskCompletionSource.Task;
    }

    private void addDialog(Type type, Dictionary<string, object> parameters)
    {
        dialogs.Add(new DialogDefinition { ComponentType = type, Parameters = parameters });
        OnDialogsUpdated?.Invoke();
    }

    private void removeDialog(Type componentType)
    {
        var dialog = dialogs.Find(x => x.ComponentType == componentType);
        if (dialog is not null)
        {
            dialogs.Remove(dialog);
            OnDialogsUpdated?.Invoke();
        }
    }
}
