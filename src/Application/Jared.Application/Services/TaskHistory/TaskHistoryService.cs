using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Dtos.TaskHistoryDtos;

namespace Jared.Application.Services.TaskHistory;

public class TaskHistoryService : ITaskHistoryService
{
    public List<TaskHistoryDto> GetChanged(
        TaskDetailsDto oldTaskDetails,
        TaskDetailsDto newTaskDetails)
    {
        List<TaskHistoryDto> changed = new();

        foreach (var property in typeof(TaskDetailsDto).GetProperties())
        {
            var oldProperty = oldTaskDetails.GetType().GetProperty(property.Name)!.GetValue(oldTaskDetails);
            var newProperty = newTaskDetails.GetType().GetProperty(property.Name)!.GetValue(newTaskDetails);
            var oldValue = oldProperty is null ? null : oldProperty.ToString();
            var newValue = newProperty is null ? null : newProperty.ToString();


            if (newValue != oldValue)
            {
                changed.Add(new()
                {
                    Property = property.Name,
                    OldValue = oldValue,
                    NewValue = newValue!,
                });
            }
        }

        return changed;
    }
}
