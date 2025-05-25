namespace Jared.Presentation.Components.Entities;

public class NotificationMessage(string message, string cssClass)
{
    public string Message { get; set; } = message;
    public string CssClass { get; set; } = cssClass;
}
