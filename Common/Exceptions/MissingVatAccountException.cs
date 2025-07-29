namespace Betalish.Common.Exceptions;

public class MissingVatAccountException : Exception
{
    public MissingVatAccountException()
    {
    }

    public MissingVatAccountException(string? message) : base(message)
    {
    }

    public MissingVatAccountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
