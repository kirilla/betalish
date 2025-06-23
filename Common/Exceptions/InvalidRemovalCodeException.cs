namespace Betalish.Common.Exceptions;

public class InvalidRemovalCodeException : Exception
{
    public InvalidRemovalCodeException() : base()
    {
    }

    public InvalidRemovalCodeException(string? message) : base(message)
    {
    }

    public InvalidRemovalCodeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
