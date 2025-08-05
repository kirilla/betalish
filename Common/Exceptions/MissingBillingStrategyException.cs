namespace Betalish.Common.Exceptions;

public class MissingBillingStrategyException : Exception
{
    public MissingBillingStrategyException()
    {
    }

    public MissingBillingStrategyException(string? message) : base(message)
    {
    }

    public MissingBillingStrategyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
