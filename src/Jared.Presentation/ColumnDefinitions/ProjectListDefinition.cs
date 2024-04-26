using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Enums;
using Jared.Presentation.ColumnDefinitions.Abstraction;

namespace Jared.Presentation.ColumnDefinitions;

public class ProjectListDefinition : ListDefinitionBase<ProjectListDto>
{
    public IColumnDefinition<ProjectListDto> Id { get; } =
    new ColumnDefinition<ProjectListDto>("Id", x => x.Id, ColumnAlignment.Left, "1", null);

    public IColumnDefinition<ProjectListDto> Title { get; } =
        new ColumnDefinition<ProjectListDto>("Title", x => x.Title, ColumnAlignment.Left, "4", null);

    public IColumnDefinition<ProjectListDto> Code { get; } =
        new ColumnDefinition<ProjectListDto>("Code", x => x.Code, ColumnAlignment.Left, "3", null);

    public override IEnumerable<IColumnDefinition<ProjectListDto>> GetColumns()
    {
        yield return Id;
        yield return Title;
        yield return Code;
    }
}
