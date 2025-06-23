namespace Betalish.Common.Exceptions;

public class BlockedByAddressException : Exception
{
    public BlockedByAddressException()
    {
    }

    public BlockedByAddressException(string? message) : base(message)
    {
    }

    public BlockedByAddressException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
