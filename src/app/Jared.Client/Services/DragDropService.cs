using Jared.Client.Abstractions;

namespace Jared.Client.Services;

public class DragDropService : IDragDropService
{
    public int? DraggedTaskId { get; private set; }

    public void StartDrag(int id) => DraggedTaskId = id;
    public void EndDrag() => DraggedTaskId = null;
}
