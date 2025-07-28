namespace Betalish.Application.Commands.Invoices.UpdateInvoiceAccounting;

public interface IUpdateInvoiceAccountingCommand
{
    Task Execute(IUserToken userToken, UpdateInvoiceAccountingCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
