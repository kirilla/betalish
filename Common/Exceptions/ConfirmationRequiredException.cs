namespace Betalish.Common.Exceptions;

public class ConfirmationRequiredException : Exception
{
    public ConfirmationRequiredException() : base()
    {
    }

    public ConfirmationRequiredException(string? message) : base(message)
    {
    }

    public ConfirmationRequiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
