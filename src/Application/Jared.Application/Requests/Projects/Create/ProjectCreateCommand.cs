using Jared.Shared.Dtos.ProjectDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Create;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
