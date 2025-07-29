using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Queues.IpAddressRateLimiting;

public class IpAddressRateLimiter(
    IDateService dateService,
    ILogItemList logItemList) : IIpAddressRateLimiter
{
    private List<IpAddressEndpointHit> list { get; set; } = [];

    public void TryRateLimit(int limit, IpAddressEndpointHit hit)
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
                        $"rate limited on endpoint {hit.Endpoint}.",
                    LogItemKind = LogItemKind.IpAddressRateLimited,
                    IpAddress = hit.IpAddress,
                });

                throw new RateLimitedException();
            }
        }
    }

    public List<IpAddressEndpointHit> ToList()
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
