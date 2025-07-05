namespace Betalish.Application.Queues.IpAddressRateLimiting;

public interface IIpAddressRateLimiter
{
    void TryRateLimit(int limit, IpAddressEndpointHit hit);

    List<IpAddressEndpointHit> ToList();

    void Clear();

    void RemoveOlderThan(TimeSpan timeSpan);
}
