using Jared.Domain.Models;
using Jared.Shared.Enums;
using Task = Jared.Domain.Models.Task;

namespace Jared.Api.Integration.Tests.Data;

public class TaskHistoryIntegrationFaker : BaseIntegrationFaker<TaskHistory>
{
    protected override int PRIORITY => 6;

    public static TaskHistory FirstTaskHistory => new()
    {
        Id = BASE_ID + 1,
        TaskId = TaskIntegrationFaker.FirstTask.Id,
        UserId = UserIntegrationFaker.FirstUser.Id,
        Property = nameof(Task.Status),
        OldValue = null,
        NewValue = ((int)Priority.Normal).ToString(),
        CreatedAt = new(2024, 3, 6, 12, 46, 3),
    };

    public static TaskHistory SecondTaskHistory => new()
    {
        Id = BASE_ID + 2,
        TaskId = TaskIntegrationFaker.FirstTask.Id,
        UserId = UserIntegrationFaker.FirstUser.Id,
        Property = nameof(Task.Title),
        OldValue = "FIRST_TASK_OLD_TITLE",
        NewValue = "FIRST_TASK_TITLE",
        CreatedAt = new(2024, 3, 6, 12, 46, 3)
    };
}
