using Jared.Core.Abstractions;
using Jared.Dtos.Auth;
using MediatR;

namespace Jared.Contracts.Auth;

public sealed record RegisterCommand(RegisterRequestDto dto)
    : IRequest<Result<bool>>;
