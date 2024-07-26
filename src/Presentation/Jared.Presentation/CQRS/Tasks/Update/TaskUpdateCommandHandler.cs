using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Tasks.Update;

public class TaskUpdateCommandHandler : IRequestHandler<TaskUpdateCommand, Result>
{
    private readonly HttpClient httpClient;

    public TaskUpdateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(TaskUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.TASK_UPDATE;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken);

        var tt = result.StatusCode;

        return Result.Ok();
    }
}
