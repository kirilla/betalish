namespace Betalish.Application.Routines.SendInvoiceEmail;

public interface ISendInvoiceEmailRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
