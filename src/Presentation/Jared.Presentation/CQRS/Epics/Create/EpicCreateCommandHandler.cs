using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Epics.Create;

public class EpicCreateCommandHandler : IRequestHandler<EpicCreateCommand, Result>
{
    private readonly HttpClient httpClient;

    public EpicCreateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(EpicCreateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_CREATE;

        var result = await httpClient.PostAsJsonAsync(baseUrl, request.dto, cancellationToken);

        return Result.Ok();
    }
}
