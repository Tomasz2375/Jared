using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.Projects.List;

public sealed record ProjectListQuery
    : IRequest<Result<List<ProjectListDto>>>;
