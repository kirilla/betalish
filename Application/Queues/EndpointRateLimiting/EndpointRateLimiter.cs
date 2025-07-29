using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Queues.EndpointRateLimiting;

public class EndpointRateLimiter(
    IDateService dateService,
    ILogItemList logItemList) : IEndpointRateLimiter
{
    private List<EndpointHit> List { get; set; } = [];

    public void TryRateLimit(int limit, EndpointHit hit)
    {
        if (!Enum.IsDefined(hit.Endpoint))
            throw new NotPermittedException();

        lock (this)
        {
            List.Add(hit);

            var count = List.Count(x => x.Endpoint == hit.Endpoint);

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
            return [.. List];
        }
    }

    public void Clear()
    {
        lock (this)
        {
            List.Clear();
        }
    }

    public void RemoveOlderThan(TimeSpan timeSpan)
    {
        lock (this)
        {
            var timeAgo = dateService.GetDateTimeNow() - timeSpan;

            List = [.. List.Where(x => x.DateTime > timeAgo)];
        }
    }
}
