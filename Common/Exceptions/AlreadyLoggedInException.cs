namespace Betalish.Common.Exceptions;

public class AlreadyLoggedInException : Exception
{
    public AlreadyLoggedInException()
    {
    }

    public AlreadyLoggedInException(string? message) : base(message)
    {
    }

    public AlreadyLoggedInException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
