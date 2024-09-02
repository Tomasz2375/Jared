using Jared.Domain.Models;

namespace Jared.Api.Integration.Tests.Data;

public class ProjectIntegrationFaker : BaseIntegrationFaker<Project>
{
    protected override int PRIORITY => 3;

    public static Project FirstProject => new()
    {
        Id = BASE_ID + 1,
        Title = "FIRST_PROJECT_TITLE",
        Description = "FIRST_PROJECT_DESCRIPTION",
        Code = "FP",
        LastTaskNumber = 1,
    };

    public static Project SecondProject => new()
    {
        Id = BASE_ID + 2,
        Title = "SECOND_PROJECT_TITLE",
        Description = "SECOND_PROJECT_DESCRIPTION",
        Code = "SP",
        LastTaskNumber = 5,
    };

    public static Project ThirdProject => new()
    {
        Id = BASE_ID + 3,
        Title = "THIRD_PROJECT_TITLE",
        Description = "THIRD_PROJECT_DESCRIPTION",
        Code = "TP",
        LastTaskNumber = 10,
    };
}
