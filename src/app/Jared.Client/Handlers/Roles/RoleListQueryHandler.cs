using System.Net.Http.Json;
using Jared.Contracts.Roles;
using Jared.Core.Abstractions;
using Jared.Dtos.Roles;
using MediatR;

namespace Jared.Client.Handlers.Roles;

public class RoleListQueryHandler(HttpClient httpClient)
    : IRequestHandler<RoleListQuery, Result<List<RoleListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<RoleListDto>>> Handle(RoleListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = BaseAdresses.ROLE_LIST;

        var response = await httpClient.GetFromJsonAsync<Result<List<RoleListDto>>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<RoleListDto>>("Invalid response type");
        }

        return response;
    }
}
