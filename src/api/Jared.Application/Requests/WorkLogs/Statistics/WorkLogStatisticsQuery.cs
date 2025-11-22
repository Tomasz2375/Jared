using Jared.Dtos.WorkLogs;
using Jared.Shared.Abstractions;
using MediatR;

namespace Jared.Application.Requests.WorkLogs.Statistics;

public sealed record WorkLogStatisticsQuery(int userId, int month, int year)
    : IRequest<Result<List<WorkLogStatisticsDto>>>;
