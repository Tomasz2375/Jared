using Jared.Application.Commands.EpicCommands;
using Jared.Domain.Abstractions;
using MediatR;
using System.Net.Http.Json;

namespace Jared.Presentation.CQRS.Epics.Update;

public class EpicUpdateCommandHandler : IRequestHandler<EpicUpdateCommand, Result>
{
    private readonly HttpClient httpClient;

    public EpicUpdateCommandHandler(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result> Handle(EpicUpdateCommand request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.EPIC_UPDATE;

        var result = await httpClient.PutAsJsonAsync(baseUrl, request.dto, cancellationToken);

        return Result.Ok();
    }
}
