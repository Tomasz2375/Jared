using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Update;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
