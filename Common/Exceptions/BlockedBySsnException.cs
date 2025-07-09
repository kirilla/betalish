namespace Betalish.Common.Exceptions;

public class BlockedBySsnException : Exception
{
    public BlockedBySsnException()
    {
    }

    public BlockedBySsnException(string? message) : base(message)
    {
    }

    public BlockedBySsnException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
