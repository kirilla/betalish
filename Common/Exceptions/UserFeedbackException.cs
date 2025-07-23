namespace Betalish.Common.Exceptions;

public class UserFeedbackException : Exception
{
    public UserFeedbackException()
    {
    }

    public UserFeedbackException(string? message) : base(message)
    {
    }

    public UserFeedbackException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
