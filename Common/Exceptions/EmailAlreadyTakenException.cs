namespace Betalish.Common.Exceptions;

public class EmailAlreadyTakenException : Exception
{
    public EmailAlreadyTakenException()
    {
    }

    public EmailAlreadyTakenException(string? message) : base(message)
    {
    }

    public EmailAlreadyTakenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
