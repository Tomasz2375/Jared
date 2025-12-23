using Jared.Client.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using MediatR;

namespace Jared.Client.Handlers.Auth;

public class RegisterCommandHandler(IApiClient apiClient)
    : IRequestHandler<RegisterCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var baseUrl = BaseAdresses.REGISTER;

        return await apiClient.PostAsync<RegisterRequestDto, bool>(baseUrl, request.dto, cancellationToken);
    }
}
