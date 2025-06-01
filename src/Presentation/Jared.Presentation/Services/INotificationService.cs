using Jared.Presentation.Components.Entities;

namespace Jared.Presentation.Services;

public interface INotificationService
{
    event Action OnChange;
    public List<NotificationMessage> Messages { get; set; }
    void RemoveNotification(NotificationMessage notification);
    void Success(string message);
    void Information(string message);
    void Warning(string message);
    void Error(string message);
}
