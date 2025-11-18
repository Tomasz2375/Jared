using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Client.Requests.User.UpdateRole;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto) : IRequest<Result<bool>>;
