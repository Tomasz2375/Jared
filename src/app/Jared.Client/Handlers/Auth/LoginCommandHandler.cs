using Jared.Client.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using MediatR;

namespace Jared.Client.Handlers.Auth;

public class LoginCommandHandler(IApiClient apiClient)
    : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await apiClient.PostAsync<LoginRequestDto, LoginResponseDto>(BaseAdresses.LOGIN, request.dto, cancellationToken);
    }
}
