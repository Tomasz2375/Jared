using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.Requests.Projects.Details;

public sealed record ProjectDetailsQuery(int id) : IRequest<Result<ProjectDetailsDto>>;
