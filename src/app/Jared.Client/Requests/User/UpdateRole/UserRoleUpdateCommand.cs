using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.User.UpdateRole;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto) : IRequest<Result<bool>>;
