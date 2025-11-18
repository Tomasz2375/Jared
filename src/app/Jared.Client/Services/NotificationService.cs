using Jared.Client.Abstractions;
using Jared.Client.Entities;
using Jared.Shared.Enums;

namespace Jared.Client.Services;

public class NotificationService : INotificationService
{
    public event Action OnChange = default!;

    public List<NotificationMessage> Messages { get; set; } = new();

    public void Success(string message)
    {
        addNotification(message, NotificationType.Success);
    }

    public void Information(string message)
    {
        addNotification(message, NotificationType.Information);
    }

    public void Warning(string message)
    {
        addNotification(message, NotificationType.Warning);
    }

    public void Error(string message)
    {
        addNotification(message, NotificationType.Error);
    }

    public void RemoveNotification(NotificationMessage notification)
    {
        Messages.Remove(notification);
        OnChange?.Invoke();
    }

    private void addNotification(string message, NotificationType type)
    {
        var cssClass = Enum.GetName(typeof(NotificationType), type)?.ToLower() ?? string.Empty;
        NotificationMessage notification = new(message, cssClass);

        Messages.Add(notification);

        OnChange.Invoke();
        Task.Run(async () =>
        {
            await Task.Delay(5000);
            RemoveNotification(notification);
        });
    }
}
