using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Jared.Contracts.Middleware;

public class RequestLogContextMiddleware(RequestDelegate requestDelegate)
{
    private readonly RequestDelegate requestDelegate = requestDelegate;

    public Task Invoke(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
        {
            return requestDelegate(httpContext);
        }
    }
}
