using Jared.Application.Dtos.TaskDtos;
using Jared.Application.Dtos.TaskHistoryDtos;

namespace Jared.Application.Services.TaskHistory;

public interface ITaskHistoryService
{
    List<TaskHistoryDto> GetChanged(
        TaskDetailsDto oldTaskDetails,
        TaskDetailsDto newTaskDetails,
        int userId);
}
