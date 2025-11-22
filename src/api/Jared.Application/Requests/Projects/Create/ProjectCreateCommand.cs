using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Create;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto)
    : IRequest<Result<bool>>;
