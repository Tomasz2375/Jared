using System.Net;
using System.Net.Http.Json;
using Jared.Client.Abstractions;
using Jared.Core.Abstractions;

namespace Jared.Client.Services;

public class ApiClient(IHttpClientFactory httpClientFactory) : IApiClient
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient("JaredApi");

    public Task<Result<TResult>> GetAsync<TResult>(
        string url,
        CancellationToken cancellationToken)
        => SendAsync<object?, TResult>(HttpMethod.Get, url, null, cancellationToken);

    public Task<Result<TResult>> PostAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken)
        => SendAsync<TRequest, TResult>(HttpMethod.Post, url, dto, cancellationToken);

    public Task<Result<TResult>> PutAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken)
        => SendAsync<TRequest, TResult>(HttpMethod.Put, url, dto, cancellationToken);

    public Task<Result<TResult>> PatchAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken)
        => SendAsync<TRequest, TResult>(HttpMethod.Patch, url, dto, cancellationToken);

    private static async Task<Result<TResult>> handleResponse<TResult>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Result.Fail<TResult>("Unauthorized");
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return Result.Ok<TResult>(default!);
        }

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<TResult>(
                $"Something went wrong. Status code: {(int)response.StatusCode} ({response.StatusCode})");
        }

        var result = await response.Content.ReadFromJsonAsync<Result<TResult>>(cancellationToken);

        return result ?? Result.Fail<TResult>("Invalid response type");
    }

    private async Task<Result<TResult>> SendAsync<TRequest, TResult>(
        HttpMethod method,
        string url,
        TRequest? body,
        CancellationToken cancellationToken)
    {
        try
        {
            using var request = new HttpRequestMessage(method, url);
            if (body is not null)
            {
                request.Content = JsonContent.Create(body);
            }

            using var response = await httpClient.SendAsync(request, cancellationToken);

            return await handleResponse<TResult>(response, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            return Result.Fail<TResult>("Request was cancelled");
        }
        catch (HttpRequestException ex)
        {
            return Result.Fail<TResult>($"Connection error: {ex.Message}");
        }
    }
}
