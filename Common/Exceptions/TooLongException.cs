namespace Betalish.Common.Exceptions;

public class TooLongException : Exception
{
    public TooLongException()
    {
    }

    public TooLongException(string? message) : base(message)
    {
    }

    public TooLongException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
