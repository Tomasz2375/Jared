using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Update;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
