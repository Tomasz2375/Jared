using System.Net.Http.Json;
using Jared.Client.Handlers;
using Jared.Contracts.Tasks;
using Jared.Core.Abstractions;
using Jared.Dtos.Tasks;
using MediatR;

namespace Jared.Client.Requests.Tasks;

public class TaskDetailsQueryHandler(HttpClient httpClient)
    : IRequestHandler<TaskDetailsQuery, Result<TaskDetailsDto>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<TaskDetailsDto>> Handle(TaskDetailsQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = $"{BaseAdresses.TASKS}/{request.id}";

        var response = await httpClient.GetFromJsonAsync<Result<TaskDetailsDto>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<TaskDetailsDto>("Invalid response type");
        }

        return response;
    }
}
