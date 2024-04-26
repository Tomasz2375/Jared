using Jared.Application.Dtos.EpicDtos;
using Jared.Domain.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions;

public class EpicListDefinition : ListDefinitionBase<EpicListDto>
{
    public IColumnDefinition<EpicListDto> Id { get; } =
        new ColumnDefinition<EpicListDto>("Id", x => x.Id, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<EpicListDto> Title { get; } =
        new ColumnDefinition<EpicListDto>("Title", x => x.Title, ColumnAlignment.Left, "4", null);

    public IColumnDefinition<EpicListDto> ParentId { get; } =
        new ColumnDefinition<EpicListDto>("ParentId", x => x.ParentId, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<EpicListDto> ProjectId { get; } =
        new ColumnDefinition<EpicListDto>("ProjectId", x => x.ProjectId, ColumnAlignment.Left, "1", null);

    public override IEnumerable<IColumnDefinition<EpicListDto>> GetColumns()
    {
        yield return Id;
        yield return Title;
        yield return ParentId;
        yield return ProjectId;
    }
}
