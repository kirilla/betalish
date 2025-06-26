namespace Betalish.Common.Exceptions;

public class UserNoLoginException : Exception
{
    public UserNoLoginException()
    {
    }

    public UserNoLoginException(string? message) : base(message)
    {
    }

    public UserNoLoginException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
