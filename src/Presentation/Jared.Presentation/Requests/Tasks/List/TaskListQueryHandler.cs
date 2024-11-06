using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.TaskDtos;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.Requests.Tasks.List;

public class TaskListQueryHandler(HttpClient httpClient)
    : IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<TaskListDto>>> Handle(TaskListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.TASK_LIST;
        string queryUrl = createQueryUrl(request.projectId, request.epicId);
        string url = baseUrl + queryUrl;

        var response = await httpClient.GetFromJsonAsync<Result<List<TaskListDto>>>(url, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<TaskListDto>>("Invalid response type");
        }

        return response;
    }

    private static string createQueryUrl(int? projectId, int? epicId)
    {
        if (projectId is null && epicId is null)
        {
            return string.Empty;
        }

        List<string> queries = new();
        if (projectId is not null)
        {
            queries.Add($"projectId={projectId}");
        }

        if (projectId is not null)
        {
            queries.Add($"epicId={epicId}");
        }

        return "?" + string.Join("&", queries);
    }
}
