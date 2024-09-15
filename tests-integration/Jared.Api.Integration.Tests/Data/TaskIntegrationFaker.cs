using Task = Jared.Domain.Models.Task;

namespace Jared.Api.Integration.Tests.Data;

public class TaskIntegrationFaker : BaseIntegrationFaker<Task>
{
    protected override int PRIORITY => 5;

    public static Task FirstTask => new()
    {
        Id = BASE_ID + 1,
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        EpicId = EpicIntegrationFaker.FirstEpic.Id,
        ParentId = null,
        Title = "FIRST_TASK_TITLE",
        Description = "FIRST_TASK_DESCRIPTION",
        Code = ProjectIntegrationFaker.FirstProject.Code + "1",
        Status = Shared.Enums.TaskStatus.Done,
        Priority = Shared.Enums.Priority.Normal,
        CreatedAt = new(2024, 3, 5, 12, 42, 15),
        StartDate = new(2024, 3, 5, 12, 45, 12),
        EndDate = new(2024, 3, 7, 11, 4, 52),
        Deadline = new(2025, 3, 31, 23, 59, 59),
        EstimatedTime = new(0, 0, 1, 0, 0),
        TotalWorkTime = new(0, 0, 0, 50, 0),
    };

    public static Task SecondTask => new()
    {
        Id = BASE_ID + 2,
        ProjectId = ProjectIntegrationFaker.FirstProject.Id,
        EpicId = EpicIntegrationFaker.FirstEpic.Id,
        ParentId = FirstTask.Id,
        Title = "SECOND_TASK_TITLE",
        Description = "SECOND_TASK_DESCRIPTION",
        Code = ProjectIntegrationFaker.FirstProject.Code + "2",
        Status = Shared.Enums.TaskStatus.Doing,
        Priority = Shared.Enums.Priority.High,
        CreatedAt = new(2024, 3, 6, 8, 4, 5),
        StartDate = new(2024, 3, 7, 8, 4, 25),
        EndDate = null,
        Deadline = new(2025, 3, 31, 23, 59, 59),
        EstimatedTime = new(0, 0, 2, 0, 0),
        TotalWorkTime = new(0, 0, 0, 30, 0),
    };
}
