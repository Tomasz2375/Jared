namespace Jared.Presentation.Services;

public class DialogDefinition
{
    public Type ComponentType { get; set; } = default!;
    public Dictionary<string, object> Parameters { get; set; } = new();
}
