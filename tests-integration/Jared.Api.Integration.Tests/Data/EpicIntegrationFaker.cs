using Jared.Domain.Models;

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
    };

    public static Epic SecondEpic => new()
    {
        Id = BASE_ID + 2,
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        ParentId = FirstEpic.Id,
        Title = "SECOND_EPIC_TITLE",
        Description = "SECOND_EPIC_DESCRIPTION",
    };

    public static Epic ThirdEpic => new()
    {
        Id = BASE_ID + 3,
        ProjectId = ProjectIntegrationFaker.SecondProject.Id,
        ParentId = null,
        Title = "THIRD_EPIC_TITLE",
        Description = "THIRS_EPIC_DESCRIPTION",
    };

    public static Epic FourthEpic => new()
    {
        Id = BASE_ID + 4,
        ProjectId = ProjectIntegrationFaker.ThirdProject.Id,
        ParentId = null,
        Title = "FOURTH_EPIC_TITLE",
        Description = "FOURTH_EPIC_DESCRIPTION",
    };
}
