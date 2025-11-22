using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto)
    : IRequest<Result<bool>>;
