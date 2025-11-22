using Jared.Dtos.Projects;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Contracts.Projects;

public sealed record ProjectListQuery
    : IRequest<Result<List<ProjectListDto>>>;
