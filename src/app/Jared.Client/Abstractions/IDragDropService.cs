namespace Jared.Client.Abstractions;

public interface IDragDropService
{
    int? DraggedTaskId { get; }

    void StartDrag(int id);
    void EndDrag();
}
