using System.Net.Http.Json;
using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Tasks.Details;

public class TaskDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>
{
    private readonly HttpClient httpClient = httpClient;

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
