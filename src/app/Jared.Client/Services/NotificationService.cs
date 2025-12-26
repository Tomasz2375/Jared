using Jared.Client.Abstractions;
using Jared.Client.Entities;
using Jared.Core.Enums;

namespace Jared.Client.Services;

public class NotificationService : INotificationService
{
    public event Action? OnChange;

    private const int MaxNotifications = 5;
    private readonly object lockNotification = new();

    public List<NotificationMessage> Messages { get; } = new();
    public void Success(string message) => _ = AddNotification(message, NotificationType.Success);
    public void Information(string message) => _ = AddNotification(message, NotificationType.Information);
    public void Warning(string message) => _ = AddNotification(message, NotificationType.Warning);
    public void Error(string message) => _ = AddNotification(message, NotificationType.Error);

    public void RemoveNotification(NotificationMessage notification)
    {
        lock (lockNotification)
        {
            if (!Messages.Contains(notification))
            {
                return;
            }

            notification.IsClosing = true;
        }

        OnChange?.Invoke();

        _ = Task.Run(async () =>
        {
            await Task.Delay(300);

            lock (lockNotification)
            {
                Messages.Remove(notification);
            }

            OnChange?.Invoke();
        });
    }

    private async Task AddNotification(string message, NotificationType type)
    {
        var cssClass = Enum.GetName(typeof(NotificationType), type)?.ToLower() ?? string.Empty;
        var notification = new NotificationMessage(message, cssClass);
        lock (lockNotification)
        {
            if (Messages.Count >= MaxNotifications)
            {
                Messages.RemoveAt(0);
            }

            Messages.Add(notification);
        }

        OnChange?.Invoke();
        await Task.Delay(5000);
        lock (lockNotification)
        {
            if (!Messages.Contains(notification))
            {
                return;
            }
        }

        RemoveNotification(notification);
    }
}
