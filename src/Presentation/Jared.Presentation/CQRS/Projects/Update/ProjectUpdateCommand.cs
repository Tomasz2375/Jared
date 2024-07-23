using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Projects.Update;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto) : IRequest<Result>;
