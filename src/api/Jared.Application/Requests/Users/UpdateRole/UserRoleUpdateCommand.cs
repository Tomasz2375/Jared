using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Users.UpdateRole;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto) : IRequest<Result<bool>>;
