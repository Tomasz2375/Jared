using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.TaskHistoryDtos;

namespace Jared.Application.Services.TaskHistory;

public class TaskHistoryService : ITaskHistoryService
{
    public List<TaskHistoryDto> GetChanged(
        TaskDetailsDto oldTaskDetails,
        TaskDetailsDto newTaskDetails,
        int userId)
    {
        List<TaskHistoryDto> changed = new();

        foreach (var property in getProperties())
        {
            var oldProperty = oldTaskDetails.GetType().GetProperty(property)?.GetValue(oldTaskDetails);
            var newProperty = newTaskDetails.GetType().GetProperty(property)?.GetValue(newTaskDetails);
            var oldValue = oldProperty is null ? null : oldProperty.ToString();
            var newValue = newProperty is null ? null : newProperty.ToString();

            if (newValue != oldValue)
            {
                changed.Add(new()
                {
                    Property = property,
                    OldValue = oldValue,
                    NewValue = newValue!,
                    UserId = userId,
                });
            }
        }

        return changed;
    }

    private static List<string> getProperties()
    {
        return new()
        {
            nameof(TaskDetailsDto.Title),
            nameof(TaskDetailsDto.Description),
            nameof(TaskDetailsDto.Code),
            nameof(TaskDetailsDto.ParentId),
            nameof(TaskDetailsDto.EpicId),
            nameof(TaskDetailsDto.ProjectId),
            nameof(TaskDetailsDto.Status),
            nameof(TaskDetailsDto.Priority),
            nameof(TaskDetailsDto.CreatedAt),
            nameof(TaskDetailsDto.StartDate),
            nameof(TaskDetailsDto.EndDate),
            nameof(TaskDetailsDto.Deadline),
            nameof(TaskDetailsDto.EstimatedTime),
            nameof(TaskDetailsDto.TotalWorkTime),
            nameof(TaskDetailsDto.CreatedById),
            nameof(TaskDetailsDto.AssignedToId),
        };
    }
}
