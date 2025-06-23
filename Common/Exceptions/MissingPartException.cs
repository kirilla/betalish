namespace Betalish.Common.Exceptions;

public class MissingPartException : Exception
{
    public MissingPartException()
    {
    }

    public MissingPartException(string? message) : base(message)
    {
    }

    public MissingPartException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
