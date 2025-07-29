namespace Betalish.Application.Commands.Invoices.UpdateInvoiceAccountingRows;

public interface IUpdateInvoiceAccountingRowsCommand
{
    Task Execute(IUserToken userToken, UpdateInvoiceAccountingRowsCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
