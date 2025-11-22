using Jared.Dtos.TaskHistories;
using Jared.Dtos.Tasks;

namespace Jared.Application.Services.TaskHistory;

public interface ITaskHistoryService
{
    List<TaskHistoryDto> GetChanged(
        TaskDetailsDto oldTaskDetails,
        TaskDetailsDto newTaskDetails,
        int userId);
}
