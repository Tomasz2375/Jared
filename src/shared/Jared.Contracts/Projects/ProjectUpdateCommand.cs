using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto)
    : IRequest<Result<bool>>;
