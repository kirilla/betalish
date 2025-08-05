namespace Betalish.Common.Exceptions;

public class MissingPaymentTermsException : Exception
{
    public MissingPaymentTermsException()
    {
    }

    public MissingPaymentTermsException(string? message) : base(message)
    {
    }

    public MissingPaymentTermsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
