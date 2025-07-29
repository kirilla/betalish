namespace Betalish.Common.Exceptions;

public class MissingRevenueAccountException : Exception
{
    public MissingRevenueAccountException()
    {
    }

    public MissingRevenueAccountException(string? message) : base(message)
    {
    }

    public MissingRevenueAccountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
