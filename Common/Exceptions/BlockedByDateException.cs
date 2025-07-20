namespace Betalish.Common.Exceptions;

public class BlockedByDateException : Exception
{
    public BlockedByDateException()
    {
    }

    public BlockedByDateException(string? message) : base(message)
    {
    }

    public BlockedByDateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
