namespace Betalish.Application.Queues.EndpointRateLimiting;

public interface IEndpointRateLimiter
{
    void TryRateLimit(int limit, EndpointHit hit);

    List<EndpointHit> ToList();

    void Clear();

    void RemoveOlderThan(TimeSpan timeSpan);
}
