namespace Betalish.Common.Exceptions;

public class MissingPasswordHashException : Exception
{
    public MissingPasswordHashException()
    {
    }

    public MissingPasswordHashException(string? message) : base(message)
    {
    }

    public MissingPasswordHashException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
