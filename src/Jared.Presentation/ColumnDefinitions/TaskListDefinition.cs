using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions;

public class TaskListDefinition : ListDefinitionBase<TaskListDto>
{
    public IColumnDefinition<TaskListDto> Id { get; } =
        new ColumnDefinition<TaskListDto>("Id", x => x.Id, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<TaskListDto> Title { get; } =
        new ColumnDefinition<TaskListDto>("Title", x => x.Title, ColumnAlignment.Left, "5", null);

    public IColumnDefinition<TaskListDto> Code { get; } =
        new ColumnDefinition<TaskListDto>("Code", x => x.Code, ColumnAlignment.Left, "2", null);

    public IColumnDefinition<TaskListDto> Priority { get; } =
        new ColumnDefinition<TaskListDto>("Priority", x => x.Priority, ColumnAlignment.Left, "2", null);

    public IColumnDefinition<TaskListDto> Status { get; } =
        new ColumnDefinition<TaskListDto>("Status", x => x.Status, ColumnAlignment.Left, "2", null);

    public IColumnDefinition<TaskListDto> CreatedAt { get; } =
        new ColumnDefinition<TaskListDto>("CreatedAt", x => x.CreatedAt, ColumnAlignment.Left, "2", "dd/MM/yyyy");

    public IColumnDefinition<TaskListDto> Deadline { get; } =
        new ColumnDefinition<TaskListDto>("Deadline", x => x.Deadline, ColumnAlignment.Left, "2", "dd/MM/yyyy");

    public override IEnumerable<IColumnDefinition<TaskListDto>> GetColumns()
    {
        yield return Id;
        yield return Title;
        yield return Code;
        yield return Priority;
        yield return Status;
        yield return CreatedAt;
        yield return Deadline;
    }
}
