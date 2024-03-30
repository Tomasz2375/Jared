﻿using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions;

public class TaskListDefinition : ListDefinitionBase<TaskListDto>
{
    public IColumnDefinition<TaskListDto> Id { get; } =
        new ColumnDefinition<TaskListDto>("Id", x => x.Id, ColumnAlignment.Left);

    public IColumnDefinition<TaskListDto> Title { get; } =
        new ColumnDefinition<TaskListDto>("Title", x => x.Title, ColumnAlignment.Left);

    public IColumnDefinition<TaskListDto> Code { get; } =
        new ColumnDefinition<TaskListDto>("Code", x => x.Code, ColumnAlignment.Left);

    public IColumnDefinition<TaskListDto> Priority { get; } =
        new ColumnDefinition<TaskListDto>("Priority", x => x.Priority, ColumnAlignment.Left);

    public IColumnDefinition<TaskListDto> Status { get; } =
        new ColumnDefinition<TaskListDto>("Status", x => x.Status, ColumnAlignment.Left);

    public override IEnumerable<IColumnDefinition<TaskListDto>> GetColumns()
    {
        yield return Id;
        yield return Title;
        yield return Code;
        yield return Priority;
        yield return Status;
    }
}
