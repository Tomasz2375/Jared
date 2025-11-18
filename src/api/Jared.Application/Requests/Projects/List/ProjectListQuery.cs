using Jared.Shared.Abstractions;
using Jared.Shared.Dtos.ProjectDtos;
using MediatR;

namespace Jared.Application.Requests.Projects.List;

public sealed record ProjectListQuery : IRequest<Result<List<ProjectListDto>>>;
