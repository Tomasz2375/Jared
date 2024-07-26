using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Presentation.CQRS.Epics.Details;
using Jared.Presentation.CQRS.Epics.List;
using Jared.Presentation.CQRS.Epics.Update;
using Jared.Presentation.CQRS.Projects.List;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class EpicDetailsForm
{
    [Parameter]
    public EventCallback CloseDialog { get; set; }

    [Parameter]
    public int Id { get; set; }

    public EpicDetailsDto Dto { get; set; } = default!;
    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private Dictionary<int, string> projectsDictionary = new();
    private Dictionary<int, string> epicsDictionary = new();

    public int ProjectId
    {
        get
        {
            return Dto.ProjectId;
        }
        set
        {
            if (value != Dto.ProjectId)
            {
                Dto.ProjectId = value;
            }
        }
    }

    public int? ParentId
    {
        get
        {
            return Dto.ParentId;
        }
        set
        {
            if (value != Dto.ParentId)
            {
                if (value == 0)
                {
                    value = null;
                }
                Dto.ParentId = value;
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await getDetails(Id);
        await getProjectsAsync();
        await getEpicsAsync();
    }

    private void cancel()
    {
        CloseDialog.InvokeAsync();
    }

    private async Task save()
    {
        var result = await Mediator.Send(new EpicUpdateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Save epic failed");
        }

        await CloseDialog.InvokeAsync();
    }

    private async Task saveAndStay()
    {
        var result = await Mediator.Send(new EpicUpdateCommand(Dto));
        if (!result.Success)
        {
            Console.WriteLine("Save epic failed");
        }
    }

    private async Task getDetails(int id)
    {
        var result = await Mediator.Send(new EpicDetailsQuery(id));

        if (!result.Success)
        {
            Console.WriteLine("Get epic failed");
        }

        Dto = result.Data;
    }

    private async Task getProjectsAsync()
    {
        var result = await Mediator.Send(new ProjectListQuery());

        if (!result.Success)
        {
            Console.WriteLine("Error when get project list");
            return;
        }

        projects = result.Data;
        projectsDictionary = projects.ToDictionary(x => x.Id, x => x.Title);
    }

    private async Task getEpicsAsync()
    {
        var result = await Mediator.Send(new EpicListQuery(Dto.ProjectId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get epic list");
            return;
        }

        epics = result.Data.ToList();
        epicsDictionary = epics.ToDictionary(x => x.Id, x => x.Title);
    }
}
