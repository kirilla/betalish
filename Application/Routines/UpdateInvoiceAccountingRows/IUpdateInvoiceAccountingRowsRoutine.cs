namespace Betalish.Application.Routines.UpdateInvoiceAccountingRows;

public interface IUpdateInvoiceAccountingRowsRoutine
{
    Task Execute(IUserToken userToken, int invoiceId);
}
