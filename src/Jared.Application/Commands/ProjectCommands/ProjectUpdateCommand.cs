using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Commands.ProjectCommands;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto) : IRequest<Result>;
