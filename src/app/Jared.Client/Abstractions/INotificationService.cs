using Jared.Client.Entities;

namespace Jared.Client.Abstractions;

public interface INotificationService
{
    event Action OnChange;
    public List<NotificationMessage> Messages { get; }
    void RemoveNotification(NotificationMessage notification);
    void Success(string message);
    void Information(string message);
    void Warning(string message);
    void Error(string message);
}
