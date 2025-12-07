using Jared.Core.Abstractions;
using Jared.Dtos.Users;
using MediatR;

namespace Jared.Contracts.Users;

public sealed record UserPasswordCommand(UserPasswordDto dto)
    : IRequest<Result<bool>>;
