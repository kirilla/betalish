namespace Betalish.Common.Exceptions;

public class BlockedByNumberException : Exception
{
    public BlockedByNumberException()
    {
    }

    public BlockedByNumberException(string? message) : base(message)
    {
    }

    public BlockedByNumberException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
