using Jared.Presentation.Components.Entities;
using Jared.Shared.Enums;

namespace Jared.Presentation.Services;

public class NotificationService(CustomDispatcher dispatcher) : INotificationService
{
    public event Action OnChange = default!;

    public List<NotificationMessage> Messages { get; set; } = new();

    public async Task Success(string message)
    {
        await addNotification(message, NotificationType.Success);
    }

    public async Task Information(string message)
    {
        await addNotification(message, NotificationType.Information);
    }

    public async Task Warning(string message)
    {
        await addNotification(message, NotificationType.Warning);
    }

    public async Task Error(string message)
    {
        await addNotification(message, NotificationType.Error);
    }

    public async Task RemoveNotification(NotificationMessage notification)
    {
        await dispatcher.InvokeAsync(() =>
        {
            Messages.Remove(notification);
            OnChange?.Invoke();
        });
    }

    private async Task addNotification(string message, NotificationType type)
    {
        var cssClass = Enum.GetName(typeof(NotificationType), type)?.ToLower() ?? string.Empty;
        NotificationMessage notification = new(message, cssClass);

        Messages.Add(notification);
        await dispatcher.InvokeAsync(OnChange.Invoke);

        await Task.Delay(5000);
        await RemoveNotification(notification);
    }
}
