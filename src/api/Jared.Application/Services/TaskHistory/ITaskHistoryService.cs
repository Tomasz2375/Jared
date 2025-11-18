using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Dtos.TaskHistoryDtos;

namespace Jared.Application.Services.TaskHistory;

public interface ITaskHistoryService
{
    List<TaskHistoryDto> GetChanged(
        TaskDetailsDto oldTaskDetails,
        TaskDetailsDto newTaskDetails,
        int userId);
}
