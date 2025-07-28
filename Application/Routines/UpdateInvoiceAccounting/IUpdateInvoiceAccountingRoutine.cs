namespace Betalish.Application.Routines.UpdateInvoiceAccounting;

public interface IUpdateInvoiceAccountingRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
