using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Queues.EndpointRateLimiting;

public class EndpointRateLimiter(
    IDateService dateService,
    ILogItemList logItemList) : IEndpointRateLimiter
{
    private List<EndpointHit> list { get; set; } = new List<EndpointHit>();

    public void TryRateLimit(int limit, EndpointHit hit)
    {
        if (!Enum.IsDefined(hit.Endpoint))
            throw new NotPermittedException();

        lock (this)
        {
            list.Add(hit);

            var count = list.Count(x => x.Endpoint == hit.Endpoint);

            if (count > limit)
            {
                logItemList.AddLogItem(new LogItem()
                {
                    Description = 
                        $"Endpoint '{hit.Endpoint}' rate limited.",
                    LogItemKind = LogItemKind.EndpointRateLimited,
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
