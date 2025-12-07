using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto)
    : IRequest<Result<bool>>;
