namespace Betalish.Common.Exceptions;

public class NullGuidException : Exception
{
    public NullGuidException()
    {
    }

    public NullGuidException(string? message) : base(message)
    {
    }

    public NullGuidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
