using Jared.Presentation.ColumnDefinitions.Abstraction;
using Jared.Shared.Dtos.EpicDtos;
using Jared.Shared.Enums;

namespace Jared.Presentation.ColumnDefinitions;

public class EpicListDefinition : ListDefinitionBase<EpicListDto>
{
    public IColumnDefinition<EpicListDto> Id { get; } =
        new ColumnDefinition<EpicListDto>("Id", typeof(int), x => x.Id, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<EpicListDto> Title { get; } =
        new ColumnDefinition<EpicListDto>("Title", typeof(string), x => x.Title, ColumnAlignment.Left, "4", null);

    public IColumnDefinition<EpicListDto> ParentId { get; } =
        new ColumnDefinition<EpicListDto>("ParentId", typeof(int?), x => x.ParentId, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<EpicListDto> ProjectId { get; } =
        new ColumnDefinition<EpicListDto>("ProjectId", typeof(int), x => x.ProjectId, ColumnAlignment.Left, "1", null);
    
    public IColumnDefinition<EpicListDto> Status { get; } =
        new ColumnDefinition<EpicListDto>("Status", typeof(EpicStatus), x => x.Status, ColumnAlignment.Left, "1", null);

    public override IEnumerable<IColumnDefinition<EpicListDto>> GetColumns()
    {
        yield return Id;
        yield return Title;
        yield return ParentId;
        yield return ProjectId;
        yield return Status;
    }
}
