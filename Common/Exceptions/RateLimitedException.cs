namespace Betalish.Common.Exceptions;

public class RateLimitedException : Exception
{
    public RateLimitedException()
    {
    }

    public RateLimitedException(string? message) : base(message)
    {
    }

    public RateLimitedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
