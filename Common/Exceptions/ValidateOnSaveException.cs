namespace Betalish.Common.Exceptions;

public class ValidateOnSaveException : Exception
{
    public ValidateOnSaveException()
    {
    }

    public ValidateOnSaveException(string? message) : base(message)
    {
    }

    public ValidateOnSaveException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
