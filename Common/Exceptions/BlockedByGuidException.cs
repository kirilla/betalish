namespace Betalish.Common.Exceptions;

public class BlockedByGuidException : Exception
{
    public BlockedByGuidException()
    {
    }

    public BlockedByGuidException(string? message) : base(message)
    {
    }

    public BlockedByGuidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
