using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Tasks.Create;

public class TaskCreateCommandHandler : IRequestHandler<TaskCreateCommand, Result>
{
    private readonly HttpClient httpClient;

    public TaskCreateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(TaskCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = "task/create";

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        return Result.Ok();
    }
}
