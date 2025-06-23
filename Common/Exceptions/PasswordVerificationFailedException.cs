namespace Betalish.Common.Exceptions;

public class PasswordVerificationFailedException : Exception
{
    public PasswordVerificationFailedException()
    {
    }

    public PasswordVerificationFailedException(string? message) : base(message)
    {
    }

    public PasswordVerificationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
