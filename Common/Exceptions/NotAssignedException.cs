namespace Betalish.Common.Exceptions;

public class NotAssignedException : Exception
{
    public NotAssignedException()
    {
    }

    public NotAssignedException(string? message) : base(message)
    {
    }

    public NotAssignedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
