namespace Betalish.Common.Exceptions;

public class BlockedByOrgnumException : Exception
{
    public BlockedByOrgnumException()
    {
    }

    public BlockedByOrgnumException(string? message) : base(message)
    {
    }

    public BlockedByOrgnumException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
