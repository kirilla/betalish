namespace Betalish.Common.Exceptions;

public class MissingAccountException : Exception
{
    public MissingAccountException()
    {
    }

    public MissingAccountException(string? message) : base(message)
    {
    }

    public MissingAccountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
