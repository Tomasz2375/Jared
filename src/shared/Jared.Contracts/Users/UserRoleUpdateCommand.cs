using Jared.Dtos.Users;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Users;

public sealed record UserRoleUpdateCommand(UserRoleUpdateDto dto)
    : IRequest<Result<bool>>;
