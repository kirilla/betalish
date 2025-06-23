namespace Betalish.Common.Exceptions;

public class BlockedByExistingException : Exception
{
    public BlockedByExistingException()
    {
    }

    public BlockedByExistingException(string? message) : base(message)
    {
    }

    public BlockedByExistingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
