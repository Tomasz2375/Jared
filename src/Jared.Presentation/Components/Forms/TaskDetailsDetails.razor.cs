﻿using Jared.Application.Dtos.EpicDtos;
using Jared.Application.Dtos.ProjectDtos;
using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Queries.EpicQueries;
using Jared.Application.Queries.ProjectQueries;
using Jared.Application.Queries.TaskQueries;
using Microsoft.AspNetCore.Components;

namespace Jared.Presentation.Components.Forms;

public partial class TaskDetailsDetails
{
    [Parameter]
    public TaskDetailsDto Dto { get; set; } = default!;

    private List<ProjectListDto> projects = new();
    private List<EpicListDto> epics = new();
    private List<TaskListDto> tasks = new();

    protected override async Task OnInitializedAsync()
    {
        await getTasksAsync();
        await getProjectsAsync();
        await getEpicsAsync();
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
    }

    private async Task getTasksAsync()
    {
        var result = await Mediator.Send(new TaskListQuery(Dto.ProjectId, Dto.EpicId));

        if (!result.Success)
        {
            Console.WriteLine("Error when get task list");
            return;
        }

        tasks = result.Data.ToList();
    }
}
