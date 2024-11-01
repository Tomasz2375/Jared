using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.UserDtos;
using MediatR;

namespace Jared.Application.Requests.Users.UpdateRole;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto) : IRequest<Result<bool>>;
