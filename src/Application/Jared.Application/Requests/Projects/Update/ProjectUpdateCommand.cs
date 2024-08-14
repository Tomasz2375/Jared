using Jared.Application.Dtos.ProjectDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Update;

public sealed record ProjectUpdateCommand(ProjectDetailsDto dto) : IRequest<Result<bool>>;
