using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Projects.Details;

public sealed record ProjectDetailsQuery(int id)
    : IRequest<Result<ProjectDetailsDto>>;
