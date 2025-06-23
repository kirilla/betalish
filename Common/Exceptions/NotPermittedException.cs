namespace Betalish.Common.Exceptions;

public class NotPermittedException : Exception
{
    public NotPermittedException()
    {
    }

    public NotPermittedException(string? message) : base(message)
    {
    }

    public NotPermittedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
