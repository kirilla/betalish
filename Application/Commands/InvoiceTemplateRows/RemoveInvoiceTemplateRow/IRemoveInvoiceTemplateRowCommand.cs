namespace Betalish.Application.Commands.InvoiceTemplateRows.RemoveInvoiceTemplateRow;

public interface IRemoveInvoiceTemplateRowCommand
{
    Task Execute(IUserToken userToken, RemoveInvoiceTemplateRowCommandModel model);

    bool IsPermitted(IUserToken userToken);
}
