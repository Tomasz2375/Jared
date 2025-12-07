using Jared.Core.Abstractions;
using Jared.Dtos.Projects;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectDetailsQuery(int id)
    : IRequest<Result<ProjectDetailsDto>>;
