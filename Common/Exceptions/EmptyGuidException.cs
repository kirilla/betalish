namespace Betalish.Common.Exceptions;

public class EmptyGuidException : Exception
{
    public EmptyGuidException()
    {
    }

    public EmptyGuidException(string? message) : base(message)
    {
    }

    public EmptyGuidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
