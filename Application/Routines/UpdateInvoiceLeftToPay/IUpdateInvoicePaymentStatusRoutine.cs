namespace Betalish.Application.Routines.UpdateInvoicePaymentStatus;

public interface IUpdateInvoicePaymentStatusRoutine
{
    Task Execute(IUserToken userToken, int invoiceDraftId);
}
