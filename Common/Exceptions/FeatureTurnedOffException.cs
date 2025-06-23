namespace Betalish.Common.Exceptions;

public class FeatureTurnedOffException : Exception
{
    public FeatureTurnedOffException()
    {
    }

    public FeatureTurnedOffException(string? message) : base(message)
    {
    }

    public FeatureTurnedOffException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
