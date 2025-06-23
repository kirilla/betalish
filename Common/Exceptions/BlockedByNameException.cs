namespace Betalish.Common.Exceptions;

public class BlockedByNameException : Exception
{
    public BlockedByNameException()
    {
    }

    public BlockedByNameException(string? message) : base(message)
    {
    }

    public BlockedByNameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
