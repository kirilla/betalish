namespace Betalish.Common.Exceptions;

public class InvalidOrgnumException : Exception
{
    public InvalidOrgnumException()
    {
    }

    public InvalidOrgnumException(string? message) : base(message)
    {
    }

    public InvalidOrgnumException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
