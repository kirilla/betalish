namespace Betalish.Application.Routines.SetInvoiceNumber;

public interface ISetInvoiceNumberRoutine
{
    Task Execute(IUserToken userToken, int invoiceDraftId);
}
