namespace Betalish.Common.Exceptions;

public class OutOfRangeException : Exception
{
    public OutOfRangeException()
    {
    }

    public OutOfRangeException(string? message) : base(message)
    {
    }

    public OutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
