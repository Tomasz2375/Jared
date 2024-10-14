using Jared.Shared.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Jared.Shared.Behaviours;

public sealed class RequestLogginPipelineBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<RequestLogginPipelineBehaviour<TRequest, TResponse>> logger;

    public RequestLogginPipelineBehaviour(ILogger<RequestLogginPipelineBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation($"Processing request {requestName}");

        TResponse result = await next();

        if (result.Success)
        {
            logger.LogInformation($"Completed request {requestName}");
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError($"Completed request {requestName} with error");
            }
        }

        return result;
    }
}
