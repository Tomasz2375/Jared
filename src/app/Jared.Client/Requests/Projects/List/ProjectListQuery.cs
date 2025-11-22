using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Projects.List;

public sealed record ProjectListQuery
    : IRequest<Result<List<ProjectListDto>>>;
