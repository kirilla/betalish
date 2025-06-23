namespace Betalish.Common.Exceptions;

public class InvalidDateRangeException : Exception
{
    public InvalidDateRangeException()
    {
    }

    public InvalidDateRangeException(string? message) : base(message)
    {
    }

    public InvalidDateRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
