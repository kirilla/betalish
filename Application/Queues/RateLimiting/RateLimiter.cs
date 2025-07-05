using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Queues.RateLimiting;

public class RateLimiter(
    IDateService dateService,
    ILogItemList logItemList) : IRateLimiter
{
    private List<EndpointHit> list { get; set; } = new List<EndpointHit>();

    public void TryRateLimit(int limit, EndpointHit hit)
    {
        if (string.IsNullOrWhiteSpace(hit.IpAddress))
            throw new NotPermittedException();

        lock (this)
        {
            list.Add(hit);

            var count = list
                .Count(x =>
                    x.Endpoint == hit.Endpoint &&
                    x.IpAddress == hit.IpAddress);

            if (count > limit)
            {
                logItemList.AddLogItem(new LogItem()
                {
                    Description = 
                        $"IP address {hit.IpAddress?.ToString() ??  "okänd"} " +
                        $"rate limitated on endpoint {hit.Endpoint}.",
                    LogItemKind = LogItemKind.RateLimited,
                    IpAddress = hit.IpAddress,
                });

                throw new RateLimitedException();
            }
        }
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
