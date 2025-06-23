namespace Betalish.Common.Exceptions;

public class MissingEmailException : Exception
{
    public MissingEmailException()
    {
    }

    public MissingEmailException(string? message) : base(message)
    {
    }

    public MissingEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
