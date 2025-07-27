namespace Betalish.Common.Exceptions;

public class AlreadyAssignedException : Exception
{
    public AlreadyAssignedException()
    {
    }

    public AlreadyAssignedException(string? message) : base(message)
    {
    }

    public AlreadyAssignedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
