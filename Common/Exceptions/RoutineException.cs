namespace Betalish.Common.Exceptions;

public class RoutineException : Exception
{
    public RoutineException()
    {
    }

    public RoutineException(string? message) : base(message)
    {
    }

    public RoutineException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
