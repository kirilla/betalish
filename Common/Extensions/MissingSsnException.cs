namespace Betalish.Common.Exceptions;

public class MissingSsnException : Exception
{
    public MissingSsnException()
    {
    }

    public MissingSsnException(string? message) : base(message)
    {
    }

    public MissingSsnException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
