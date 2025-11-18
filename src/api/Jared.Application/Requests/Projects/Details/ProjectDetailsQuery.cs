using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;

namespace Jared.Application.Requests.Projects.Details;

public sealed record ProjectDetailsQuery(int id) : IRequest<Result<ProjectDetailsDto>>;
