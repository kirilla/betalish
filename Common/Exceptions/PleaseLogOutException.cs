namespace Betalish.Common.Exceptions;

public class PleaseLogOutException : Exception
{
    public PleaseLogOutException()
    {
    }

    public PleaseLogOutException(string? message) : base(message)
    {
    }

    public PleaseLogOutException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
