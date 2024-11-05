using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Create;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
