namespace Betalish.Common.Exceptions;

public class BlockedByInvoiceException : Exception
{
    public BlockedByInvoiceException()
    {
    }

    public BlockedByInvoiceException(string? message) : base(message)
    {
    }

    public BlockedByInvoiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
