namespace Betalish.Common.Exceptions;

public class UserNoSaveException : Exception
{
    public UserNoSaveException()
    {
    }

    public UserNoSaveException(string? message) : base(message)
    {
    }

    public UserNoSaveException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
