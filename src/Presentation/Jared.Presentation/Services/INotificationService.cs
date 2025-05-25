using Jared.Presentation.Components.Entities;

namespace Jared.Presentation.Services;

public interface INotificationService
{
    event Action OnChange;
    public List<NotificationMessage> Messages { get; set; }
    Task RemoveNotification(NotificationMessage notification);
    Task Success(string message);
    Task Information(string message);
    Task Warning(string message);
    Task Error(string message);
}
