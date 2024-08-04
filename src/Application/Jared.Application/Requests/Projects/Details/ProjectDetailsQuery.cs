using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.Details;

public record ProjectDetailsQuery(int id) : IRequest<Result<ProjectDetailsDto>>;
