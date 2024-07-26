using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.ProjectCommands;

public sealed record ProjectCreateCommand(ProjectDetailsDto dto) : IRequest<Result>;
