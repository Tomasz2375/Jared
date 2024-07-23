using Jared.Application.Dtos.TaskDtos;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;
using System.Text;

namespace Jared.Presentation.CQRS.Tasks.List;

public class TaskListQueryHandler : IRequestHandler<TaskListQuery, Result<List<TaskListDto>>>
{
    private readonly HttpClient httpClient;

    public TaskListQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<List<TaskListDto>>> Handle(TaskListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = "task/list";
        string queryUrl = createQueryUrl(request.projectId, request.epicId);
        string url = baseUrl + queryUrl;

        return await httpClient.GetFromJsonAsync<Result<List<TaskListDto>>>(url);

    }

    private string createQueryUrl(int? projectId, int? epicId)
    {
        StringBuilder queryBuilder = new();

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
