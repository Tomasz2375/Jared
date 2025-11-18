using Jared.Client.ColumnDefinitions;
using Jared.Client.Requests.Epics.Page;
using Jared.Shared.Dtos.EpicDtos;

namespace Jared.UI.Pages;

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
        var result = await Mediator.Send(new EpicPageQuery(query));

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
