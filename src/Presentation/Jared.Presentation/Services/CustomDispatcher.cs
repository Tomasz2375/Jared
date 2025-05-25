using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Services;

public class CustomDispatcher : Dispatcher
{
    private readonly SynchronizationContext synchronizationContext;

    public CustomDispatcher()
    {
        synchronizationContext = SynchronizationContext.Current ?? throw new InvalidOperationException(
            "SynchronizationContext is not available. Ensure this class is used within a Blazor component.");
    }

    public override bool CheckAccess()
    {
        return SynchronizationContext.Current == synchronizationContext;
    }

    public override Task InvokeAsync(Action workItem)
    {
        if (CheckAccess())
        {
            workItem();

            return Task.CompletedTask;
        }

        return Task.Run(() => synchronizationContext.Post(_ => workItem(), null));
    }

    public override Task InvokeAsync(Func<Task> workItem)
    {
        if (CheckAccess())
        {
            return workItem();
        }

        TaskCompletionSource taskCompletionSource = new();
        synchronizationContext.Post(
            async _ =>
            {
                await workItem();
                taskCompletionSource.SetResult();
            }, null);

        return taskCompletionSource.Task;
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
    {
        if (CheckAccess())
        {
            return Task.FromResult(workItem());
        }

        var tcs = new TaskCompletionSource<TResult>();
        synchronizationContext.Post(_ => tcs.SetResult(workItem()), null);

        return tcs.Task;
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
    {
        if (CheckAccess())
        {
            return workItem();
        }

        TaskCompletionSource<TResult> taskCompletionSource = new();
        synchronizationContext.Post(
            async _ =>
            {
                var result = await workItem();
                taskCompletionSource.SetResult(result);
            }, null);

        return taskCompletionSource.Task;
    }
}
