namespace Betalish.Common.Exceptions;

public class InvalidSsnException : Exception
{
    public InvalidSsnException()
    {
    }

    public InvalidSsnException(string? message) : base(message)
    {
    }

    public InvalidSsnException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
