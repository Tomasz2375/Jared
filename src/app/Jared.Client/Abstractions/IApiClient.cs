using Jared.Core.Abstractions;

namespace Jared.Client.Abstractions;

public interface IApiClient
{
    Task<Result<T>> GetAsync<T>(string url, CancellationToken cancellationToken);
    Task<Result<TResult>> PostAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken);
    Task<Result<TResult>> PutAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken);
    Task<Result<TResult>> PatchAsync<TRequest, TResult>(
        string url,
        TRequest dto,
        CancellationToken cancellationToken);
}
