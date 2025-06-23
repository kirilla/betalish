namespace Betalish.Common.Exceptions;

public class MissingNameException : Exception
{
    public MissingNameException()
    {
    }

    public MissingNameException(string? message) : base(message)
    {
    }

    public MissingNameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
