using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Presentation.CQRS.Projects.Details;

public sealed record ProjectDetailsQuery(int id) : IRequest<Result<ProjectDetailsDto>>;
