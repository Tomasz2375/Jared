using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Create;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
