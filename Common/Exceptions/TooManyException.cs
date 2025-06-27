namespace Betalish.Common.Exceptions;

public class TooManyException : Exception
{
    public TooManyException()
    {
    }

    public TooManyException(string? message) : base(message)
    {
    }

    public TooManyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
