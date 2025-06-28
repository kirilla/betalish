namespace Betalish.Common.Exceptions;

public class BlockedByEmailException : Exception
{
    public BlockedByEmailException()
    {
    }

    public BlockedByEmailException(string? message) : base(message)
    {
    }

    public BlockedByEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
