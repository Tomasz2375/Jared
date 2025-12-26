namespace Jared.Client.Entities;

public class NotificationMessage
{
    public Guid Id { get; }
    public string Message { get; }
    public string CssClass { get; }
    public bool IsClosing { get; set; }

    public NotificationMessage(string message, string cssClass)
    {
        Id = Guid.NewGuid();
        Message = message;
        CssClass = cssClass;
    }
}
