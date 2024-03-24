using Jared.Application.Dtos.ProjectDtos;
using Jared.Domain.Abstractions;
using MediatR;

namespace Jared.Application.Queries.ProjectQueries
{
    public sealed record ProjectListQuery() : IRequest<Result<List<ProjectListDto>>>;
}
