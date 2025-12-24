using System.Net;
using Jared.Core.Abstractions;
using Jared.Dtos.Auth;

namespace Jared.App;

public class AuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    : DelegatingHandler
{
    private const string ACCESS_TOKEN = "ACCESS_TOKEN";

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var path = httpContext.Request.Path.Value ?? string.Empty;
        if (path.StartsWith("/authorization/", StringComparison.OrdinalIgnoreCase))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var accessToken = httpContext.Items[ACCESS_TOKEN] as string;
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        accessToken = await TryRefreshAccessTokenAsync(httpContext, cancellationToken);
        if (string.IsNullOrEmpty(accessToken))
        {
            return response;
        }

        httpContext.Items[ACCESS_TOKEN] = accessToken;
        request.Headers.Authorization = new("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }

    private static async Task<string> TryRefreshAccessTokenAsync(
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        try
        {
            var cookieName = httpContext
                .RequestServices
                .GetRequiredService<IWebHostEnvironment>()
                .IsDevelopment()
                ? "refresh_token_dev"
                : "refresh_token";
            var refreshToken = httpContext.Request.Cookies[cookieName];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return string.Empty;
            }

            var client = httpContext
                .RequestServices
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("JaredApi");
            RefreshTokenDto dto = new() { RefreshToken = refreshToken };
            var response = await client.PostAsJsonAsync("auth/refresh", dto, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            var result = await response.Content.ReadFromJsonAsync<Result<string>>(cancellationToken);
            if (result?.Success != true || result.Data is null)
            {
                return string.Empty;
            }

            return result.Data;
        }
        catch
        {
            return string.Empty;
        }
    }
}
