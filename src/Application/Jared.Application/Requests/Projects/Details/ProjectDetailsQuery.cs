using Jared.Application.Dtos.ProjectDtos;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Details;

public sealed record ProjectDetailsQuery(int id) : IRequest<Result<ProjectDetailsDto>>;
