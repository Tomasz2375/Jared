using Jared.Application.Dtos.EpicDtos;
using Jared.Presentation.ColumnDefinitions;
using Jared.Presentation.CQRS.Epics.Page;

namespace Jared.Presentation.Pages;

public partial class Epics
{
    public EpicPageDto Model { get; set; } = new();
    public Query Query { get; set; } = new();
    private int showDialogWithId;

    protected override async Task OnInitializedAsync()
    {
        await sendPageQuery(Query);
    }

    private async Task sendPageQuery(Query query)
    {
        var result = await Mediator.Send(new EpicPageQuery(query));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
