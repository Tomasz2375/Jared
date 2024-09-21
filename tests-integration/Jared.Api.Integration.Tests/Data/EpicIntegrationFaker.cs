using Jared.Domain.Models;
using Jared.Shared.Enums;

namespace Jared.Api.Integration.Tests.Data;

public class EpicIntegrationFaker : BaseIntegrationFaker<Epic>
{
    protected override int PRIORITY => 4;

    public static Epic FirstEpic => new()
    {
        Id = BASE_ID + 1,
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        ParentId = null,
        Title = "FIRST_EPIC_TITLE",
        Description = "FIRST_EPIC_DESCRIPTION",
        Status = EpicStatus.ToDo,
        CreatedAt = new(2024, 9, 20, 12, 0, 0),
        StartDate = new(2024, 9, 21, 13, 0, 0),
        Deadline = new(2024, 9, 29, 14, 0, 0),
        EndDate = null,
    };

    public static Epic SecondEpic => new()
    {
        Id = BASE_ID + 2,
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        ParentId = FirstEpic.Id,
        Title = "SECOND_EPIC_TITLE",
        Description = "SECOND_EPIC_DESCRIPTION",
        Status = EpicStatus.Doing,
        CreatedAt = new(2024, 9, 14, 13, 5, 0),
        StartDate = new(2024, 9, 15, 13, 10, 0),
        Deadline = new(2024, 9, 30, 13, 15, 0),
        EndDate = null,
    };

    public static Epic ThirdEpic => new()
    {
        Id = BASE_ID + 3,
        ProjectId = ProjectIntegrationFaker.SecondProject.Id,
        ParentId = null,
        Title = "THIRD_EPIC_TITLE",
        Description = "THIRS_EPIC_DESCRIPTION",
        Status = EpicStatus.ToDo,
        CreatedAt = new(2024, 9, 1, 8, 0, 1),
        StartDate = new(2024, 9, 2, 9, 0, 2),
        Deadline = new(2024, 9, 16, 10, 0, 3),
        EndDate = new(2024, 9, 14, 11, 0, 4),
    };

    public static Epic FourthEpic => new()
    {
        Id = BASE_ID + 4,
        ProjectId = ProjectIntegrationFaker.ThirdProject.Id,
        ParentId = null,
        Title = "FOURTH_EPIC_TITLE",
        Description = "FOURTH_EPIC_DESCRIPTION",
        Status = EpicStatus.Canceled,
        CreatedAt = new(2024, 8, 24, 14, 20, 7),
        StartDate = null,
        Deadline = null,
        EndDate = null,
    };
}
