using Betalish.Application.Queues.LogItems;

namespace Betalish.Application.Queues.SignInRateLimiting;

public class SignInRateLimiter(
    IDateService dateService,
    ILogItemList logItemList) : ISignInRateLimiter
{
    private List<SignInAttempt> list { get; set; } = [];

    public void TryRateLimit(int limit, SignInAttempt attempt)
    {
        if (string.IsNullOrWhiteSpace(attempt.Username))
            throw new NotPermittedException();

        lock (this)
        {
            list.Add(attempt);

            var count = list
                .Count(x =>
                    x.Endpoint == attempt.Endpoint &&
                    x.Username == attempt.Username);

            if (count > limit)
            {
                logItemList.AddLogItem(new LogItem()
                {
                    Description = 
                        $"Username '{attempt.Username}' " +
                        $"rate limited on endpoint {attempt.Endpoint}.",
                    LogItemKind = LogItemKind.SignInRateLimited,
                });

                throw new RateLimitedException();
            }
        }
    }

    public List<SignInAttempt> ToList()
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
