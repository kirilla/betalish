namespace Betalish.Application.Queues.RateLimiting;

public class RateLimiter(IDateService dateService) : IRateLimiter
{
    private List<EndpointHit> list { get; set; } = new List<EndpointHit>();

    public void TryRateLimit(EndpointHit hit)
    {
        lock (this)
        {
            list.Add(hit);
        }

        Evaluate(hit);
    }

    private void Evaluate(EndpointHit hit)
    {
        int count = 0;

        lock (this)
        {
            count = list
                .Count(x =>
                    x.Endpoint == hit.Endpoint &&
                    x.IpAddress == hit.IpAddress);
        }

        if (count > 10)
            throw new RateLimitedException();
    }

    public List<EndpointHit> ToList()
    {
        lock (this)
        {
            return list.ToList();
        }
    }

    public void Clear()
    {
        lock (this)
        {
            list.Clear();
        }
    }

    public void RemoveOlderThan(TimeSpan timeSpan)
    {
        lock (this)
        {
            var timeAgo = dateService.GetDateTimeNow() - timeSpan;

            list = list
                .Where(x => x.DateTime > timeAgo)
                .ToList();
        }
    }
}
