namespace Betalish.Common.Exceptions;

public class PleaseSignInException : Exception
{
    public PleaseSignInException()
    {
    }

    public PleaseSignInException(string? message) : base(message)
    {
    }

    public PleaseSignInException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
