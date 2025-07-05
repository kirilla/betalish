namespace Betalish.Application.Queues.RateLimiting;

public interface IRateLimiter
{
    void TryRateLimit(int limit, EndpointHit hit);

    List<EndpointHit> ToList();

    void Clear();

    void RemoveOlderThan(TimeSpan timeSpan);
}
