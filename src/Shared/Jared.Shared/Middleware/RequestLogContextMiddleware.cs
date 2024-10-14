using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Jared.Shared.Middleware;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate requestDelegate;

    public RequestLogContextMiddleware(RequestDelegate requestDelegate)
    {
        this.requestDelegate = requestDelegate;
    }

    public Task Invoke(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
        {
            return requestDelegate(httpContext);
        }
    }
}
