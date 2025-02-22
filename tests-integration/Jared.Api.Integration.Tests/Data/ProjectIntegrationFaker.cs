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
        Color = "#AA2244",
        LastTaskNumber = 1,
    };

    public static Project SecondProject => new()
    {
        Id = BASE_ID + 2,
        Title = "SECOND_PROJECT_TITLE",
        Description = "SECOND_PROJECT_DESCRIPTION",
        Code = "SP",
        Color = "#44AA22",
        LastTaskNumber = 5,
    };

    public static Project ThirdProject => new()
    {
        Id = BASE_ID + 3,
        Title = "THIRD_PROJECT_TITLE",
        Description = "THIRD_PROJECT_DESCRIPTION",
        Code = "TP",
        Color = "#44AA22",
        LastTaskNumber = 10,
    };
}
