using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Presentation.ColumnDefinitions;

namespace Jared.Presentation.Pages;

public partial class Epics
{
    public EpicPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new EpicPageQuery(
            query.Page,
            query.PageSize,
            query.SortingProperty,
            query.SortingDirection,
            query.Filter));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
