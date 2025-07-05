namespace Betalish.Application.Queues.SignInRateLimiting;

public interface ISignInRateLimiter
{
    void TryRateLimit(int limit, SignInAttempt attempt);

    List<SignInAttempt> ToList();

    void Clear();

    void RemoveOlderThan(TimeSpan timeSpan);
}
