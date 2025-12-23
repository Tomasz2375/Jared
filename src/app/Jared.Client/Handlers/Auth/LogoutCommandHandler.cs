using Jared.Client.Abstractions;
using Jared.Contracts.Auth;
using Jared.Core.Abstractions;
using MediatR;

namespace Jared.Client.Handlers.Auth;

public class LogoutCommandHandler(IApiClient apiClient)
    : IRequestHandler<LogoutCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return apiClient.PostAsync<object, bool>(BaseAdresses.LOGOUT, request.dto, cancellationToken);
    }
}
