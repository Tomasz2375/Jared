using Jared.Dtos.Tasks;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Client.Requests.Tasks.Details;

public sealed record TaskDetailsQuery(int id)
    : IRequest<Result<TaskDetailsDto>>;
