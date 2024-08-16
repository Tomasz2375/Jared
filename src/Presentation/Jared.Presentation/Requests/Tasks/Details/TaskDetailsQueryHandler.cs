using Jared.Shared.Dtos.TaskDtos;
using Jared.Shared.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.Requests.Tasks.Details;

public class TaskDetailsQueryHandler : IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>
{
    private readonly HttpClient httpClient;

    public TaskDetailsQueryHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<TaskDetailsDto>> Handle(TaskDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.TASK_DETAILS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<TaskDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<TaskDetailsDto>("Invalid response type");
        }

        return response;
    }
}
